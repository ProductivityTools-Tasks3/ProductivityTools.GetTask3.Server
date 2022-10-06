using ProductivityTools.DateTimeTools;
using ProductivityTools.GetTask3.CoreObjects.Tomato;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Repositories
{
    public static class SearchConditions
    {
        public static Func<Element, int?, IDateTimePT, bool> GetTodaysList = (l, rootId, _dateTimePT) =>
             l.Status != Status.Deleted &&
                (
                    (l.ParentId == rootId && l.Status != Status.Finished && l.Initialization <= _dateTimePT.Now.AddDays(1).Date.AddSeconds(-1)) ||
                    (l.ParentId == rootId && l.Status == Status.Finished && l.Finished.Value.Date == _dateTimePT.Now.Date)
                );

        public static Func<Element, int?, IDateTimePT, bool> GetFinshedThisWeek = (l, rootId, _dateTimePT) =>
            l.Status != Status.Deleted &&
                 (
                     (l.ParentId == rootId && l.Status != Status.Finished && l.Initialization <= _dateTimePT.Now.AddDays(1).Date.AddSeconds(-1)) ||
                     (l.ParentId == rootId && l.Status == Status.Finished && _dateTimePT.Now.AddDays(-1 * _dateTimePT.Now.DayOfYear).Date < l.Finished.Value.Date)
                 );

    }
}
