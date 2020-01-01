using System;
using System.Collections.Concurrent;

namespace ObjectPoolBlog
{
    public class ObjectPool<T>
    {
        private ConcurrentBag<T> _object;
        private Func<T> _objectGenerator;

        public ObjectPool(Func<T> objectGenerator) {
            _object = new ConcurrentBag<T>();
            _objectGenerator = objectGenerator;
        }
        /// <summary>
        ///     取出
        /// </summary>
        /// <returns></returns>
        public T CheckOut() {
            T item;
            if (_object.TryTake(out item)) return item;
            return _objectGenerator();
        }
        /// <summary>
        ///     归还
        /// </summary>
        /// <param name="obj"></param>
        public void CheckIn(T obj) {
             _object.Add(obj);
        }

    }
}
