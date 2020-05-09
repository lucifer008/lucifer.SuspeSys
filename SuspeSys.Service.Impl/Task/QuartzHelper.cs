using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Task
{
    public class QuartzHelper
    {
        /// <summary>
        /// 时间间隔执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="seconds">时间间隔(单位：毫秒)</param>
        public static void ExecuteInterval<T>(int seconds) where T : IJob
        {
            // Grab the Scheduler instance from the Factory
            NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            Task<IScheduler> scheduler = factory.GetScheduler();

            //IJobDetail job = JobBuilder.Create<T>().WithIdentity("job1", "group1").Build();
            IJobDetail job = JobBuilder.Create<T>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .StartAt(DateTime.Now.AddSeconds(seconds))
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(seconds).RepeatForever())
                .Build();

            scheduler.Result.ScheduleJob(job, trigger);

            scheduler.Result.Start();
        }

        /// <summary>
        /// 指定时间执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="cronExpression">cron表达式，即指定时间点的表达式</param>
        public static void ExecuteByCron<T>(string cronExpression) where T : IJob
        {
            // Grab the Scheduler instance from the Factory
            NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            Task<IScheduler> scheduler = factory.GetScheduler();

            IJobDetail job = JobBuilder.Create<T>().Build();

            //DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(DateTime.Now.AddSeconds(1), 2);
            //DateTimeOffset endTime = DateBuilder.NextGivenSecondDate(DateTime.Now.AddYears(2), 3);

            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                //.StartAt(startTime).EndAt(endTime)
                .WithCronSchedule(cronExpression)
                .Build();

            scheduler.Result.ScheduleJob(job, trigger);

            scheduler.Result.Start();
        }
    }
}
