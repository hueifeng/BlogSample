using Newtonsoft.Json;
using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RulesEngineDemo
{
    class Program
    {
        private static readonly ReSettings reSettings = new ReSettings
        {
            CustomTypes = new[] { typeof(IdCardUtil) }
        };

        static async Task Main(string[] args)
        {
            //定义规则
            var rulesStr = @"[{
                    ""WorkflowName"": ""UserInputWorkflow"",
                    ""Rules"": [
                      {
                        ""RuleName"": ""CheckNestedSimpleProp"",
                        ""ErrorMessage"": ""Value值不是second."",
                        ""ErrorType"": ""Error"",
                        ""RuleExpressionType"": ""LambdaExpression"",
                        ""Expression"": ""user.UserId==1 && items[0].Value==second""
                      }
                    ]
                  }] ";


            var userInput = new UserInput
            {
                UserId = 1,
                IdNo = "11010519491230002X",
                Age = 18
            };

            var input = new
            {
                user = userInput,
                items = new List<ListItem>()
                {
                    new ListItem{ Id=1,Value="first"},
                    new ListItem{ Id=2,Value="second"}
                }
            };


            //反序列化Json格式规则字符串
            var workflowRules = JsonConvert.DeserializeObject<List<WorkflowRules>>(rulesStr);
            var rulesEngine = new RulesEngine.RulesEngine(workflowRules.ToArray(), null, reSettings: reSettings);

            List<RuleResultTree> resultList = await rulesEngine.ExecuteAllRulesAsync("UserInputWorkflow", input);
            foreach (var item in resultList)
            {
                //Console.WriteLine("验证成功：{0}，消息：{1}", item.IsSuccess, item.ExceptionMessage);
            }


            Expression<Func<ListItem, bool>> predicate = x => x.Id == 1;
            //输入条件如下
            var inputItem = new ListItem
            {
                Id = 1,
                Value = "second"
            };

            if (inputItem.Id !=null)
            {
                predicate = predicate.And(x=>x.Id==inputItem.Id);
            }

            if (inputItem.Id != null)
            {
                predicate = predicate.And(x => x.Value == inputItem.Value);
            }


            var items = input.items.AsQueryable().Where("Id ==@0  && Value==@1",inputItem.Id,inputItem.Value).ToList();



            foreach (var item in items)
            {
                Console.WriteLine($"Id：{item.Id},Value: {item.Value}");
            }

            Console.ReadLine();

        }


        public class UserInput
        {
            public int UserId { get; set; }
            public string IdNo { get; set; }
            public int Age { get; set; }
        }

        public class ListItem
        {
            public int? Id { get; set; }

            public string Value { get; set; }
        }
    }


    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }

    public static class IdCardUtil
    {
        public static int GetAgeByIdCard(this string idCard)
        {
            int age = 0;
            if (!string.IsNullOrWhiteSpace(idCard))
            {
                var subStr = string.Empty;
                if (idCard.Length == 18)
                {
                    subStr = idCard.Substring(6, 8).Insert(4, "-").Insert(7, "-");
                }
                else if (idCard.Length == 15)
                {
                    subStr = ("19" + idCard.Substring(6, 6)).Insert(4, "-").Insert(7, "-");
                }
                TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(subStr));
                age = ts.Days / 365;
            }
            return age;
        }

    }
}
