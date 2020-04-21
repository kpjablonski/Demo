using Quartz;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tenders.Startup
{
    public class QuartzJobRegistry : IEnumerable<KeyValuePair<Type, Action<SimpleScheduleBuilder>>>
    {
        private readonly Dictionary<Type, Action<SimpleScheduleBuilder>> registry = new Dictionary<Type, Action<SimpleScheduleBuilder>>();
        public void Repeat<T>() where T : IJob
        {
            registry.Add(typeof(T), options => options.WithIntervalInSeconds(60).RepeatForever());
        }
        public IEnumerator<KeyValuePair<Type, Action<SimpleScheduleBuilder>>> GetEnumerator() => registry.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => registry.GetEnumerator();
    }
}
