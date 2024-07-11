using ProductivityTools.DateTimeTools;
using ProductivityTools.GetTask3.CoreObjects.Tomato;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Repositories
{
    public class SearchConditions
    {
        public static string GetTodaysList = "GetTodaysList";
        public static string GetFinshedThisWeek = "GetFinshedThisWeek";
        public static string GetFinshedLast7Days = "GetFinshedLast7Days";
        static int rootId { get; set; }
        public SearchConditions(int rootId)
        {
            SearchConditions.rootId= rootId;
        }

        public  Func<Element, bool> GetTodaysList2 = (l) =>
             l.Status != Status.Deleted &&
                (
                    (l.ParentId == rootId && l.Status != Status.Finished && l.Initialization <= DateTime.Now.AddDays(1).Date.AddSeconds(-1)) ||
                    (l.ParentId == rootId && l.Status == Status.Finished && l.Finished.Value.Date == DateTime.Now.Date)
                );

        public Func<Element, bool> GetFinshedThisWeek2 = (l) =>
            l.Status != Status.Deleted &&
                 (
                     (l.ParentId == rootId && l.Status != Status.Finished && l.Initialization <= DateTime.Now.AddDays(1).Date.AddSeconds(-1)) ||
                     (l.ParentId == rootId && l.Status == Status.Finished && DateTime.Now.AddDays(-1 * DateTime.Now.DayOfYear).Date < l.Finished.Value.Date)
                 );

    }
}
