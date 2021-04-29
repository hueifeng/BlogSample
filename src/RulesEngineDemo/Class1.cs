//using Newtonsoft.Json;
//using RulesEngine.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RulesEngineDemo
//{
//    class Program
//    {
//        private static readonly ReSettings reSettings = new ReSettings
//        {
//            CustomTypes = new[] { typeof(IdCardUtil) }
//        };

//        static async Task Main(string[] args)
//        {
//            //定义规则
//            var rulesStr = @"[{
//                    ""WorkflowName"": ""UserInputWorkflow"",
//                    ""Rules"": [
//                      {
//                        ""RuleName"": ""CheckNestedSimpleProp"",
//                        ""ErrorMessage"": ""年龄必须小于18岁."",
//                        ""ErrorType"": ""Error"",
//                        ""RuleExpressionType"": ""LambdaExpression"",
//                        ""Expression"": ""IdNo.GetAgeByIdCard() < 18""
//                      },
//                       {
//                        ""RuleName"": ""CheckNestedSimpleProp1"",
//                        ""ErrorMessage"": ""身份证号不可以为空."",
//                         ""ErrorType"": ""Error"",
//                        ""RuleExpressionType"": ""LambdaExpression"",
//                        ""Expression"": ""IdNo != null""
//                      }
//                    ]
//                  }] ";

//            //规则JSON架构
//            //RuleName规则名称
//            //Properties 规则属性，获取或设置规则的自定义属性或标记。
//            //Operator 操作符
//            //ErrorMessage 错误消息
//            //Enabled 获取和设置规则是否已启用
//            //RuleExpressionType 默认RuleExpressionType.LambdaExpression
//            //WorkflowRulesToInject 注入工作规则
//            //Rules 规则
//            //LocalParams 
//            //Expression 表达树
//            //Actions 
//            //SuccessEvent 完成事件，默认为规则名称



//            var userInput = new UserInput
//            {
//                IdNo = "11010519491230002X",
//                Age = 18
//            };

//            //反序列化Json格式规则字符串
//            var workflowRules = JsonConvert.DeserializeObject<List<WorkflowRules>>(rulesStr);



//            var rulesEngine = new RulesEngine.RulesEngine(workflowRules.ToArray(), null, reSettings: reSettings);

//            List<RuleResultTree> resultList = await rulesEngine.ExecuteAllRulesAsync("UserInputWorkflow", userInput);
//            foreach (var item in resultList)
//            {
//                //{ "Rule":{ "RuleName":"CheckNestedSimpleProp","Properties":null,"Operator":null,"ErrorMessage":"年龄必须大于18岁.",
//                //"ErrorType":"Error","RuleExpressionType":"LambdaExpression","WorkflowRulesToInject":null,"Rules":null,"LocalParams":null,"Expression":"Age > 18","Actions":null,"SuccessEvent":null},"IsSuccess":false,"ChildResults":null,"Inputs":{ "input1":{ "IdNo":null,"Age":18} },
//                //"ActionResult":{ "Output":null,"Exception":null},"ExceptionMessage":"年龄必须大于18岁.","RuleEvaluatedParams":[]}


//                Console.WriteLine("验证成功：{0}，消息：{1}", item.IsSuccess, item.ExceptionMessage);
//            }

//            Console.ReadLine();

//        }


//        public class UserInput
//        {
//            public int UserId { get; set; }
//            public string IdNo { get; set; }
//            public int Age { get; set; }
//        }

//        public class Goods
//        {
//            public int Id { get; set; }

//            public int Name { get; set; }

//            public int UserId { get; set; }
//        }
//    }

//    public static class IdCardUtil
//    {
//        public static int GetAgeByIdCard(this string idCard)
//        {
//            int age = 0;
//            if (!string.IsNullOrWhiteSpace(idCard))
//            {
//                var subStr = string.Empty;
//                if (idCard.Length == 18)
//                {
//                    subStr = idCard.Substring(6, 8).Insert(4, "-").Insert(7, "-");
//                }
//                else if (idCard.Length == 15)
//                {
//                    subStr = ("19" + idCard.Substring(6, 6)).Insert(4, "-").Insert(7, "-");
//                }
//                TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(subStr));
//                age = ts.Days / 365;
//            }
//            return age;
//        }

//    }
//}
