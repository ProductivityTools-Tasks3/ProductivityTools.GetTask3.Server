using ProductivityTools.GetTask3.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.GetTomatoReport
{
    public class Report : PSCmdlet.PSCommandPT<GetTomatoReportCmdlet>
    {
        string format = @"hh\:mm";

        public Report(GetTomatoReportCmdlet cmdletType) : base(cmdletType)
        {
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            var task = TaskStructureFactory.Get(this.Cmdlet);
            var result = task.GetTomatoReport(DateTime.Now.AddDays(-1 * this.Cmdlet.Ago));
            TimeSpan allTomatoTime = TimeSpan.Zero;
            foreach (var r in result.Result.Tomatoes)
            {
                DateTime tomatoFinished = r.Finished ?? DateTime.Now;
                TimeSpan curentTomatoTime = tomatoFinished - r.Created;
                allTomatoTime = allTomatoTime.Add(curentTomatoTime);
                string titles = string.Join(",", r.Elements.Select(x => x.Name));
                WriteOutput($"{r.Created.ToString(format)}-{tomatoFinished.ToString(format)}, {curentTomatoTime.ToString(format)} :: {allTomatoTime.ToString(format)} [{titles}]");
            }
        }
    }
}
