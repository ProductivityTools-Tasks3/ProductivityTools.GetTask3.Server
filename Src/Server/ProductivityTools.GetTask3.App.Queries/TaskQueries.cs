using AutoMapper;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.Contract.Responses;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductivityTools.GetTask3.App.Queries
{
    public interface ITaskQueries
    {
        ElementView GetTaskList(int? bagId, string path, string userName);
        ElementView GetTaskListFinishedThisWeek(int? bagId, string path, string userName);
        int? GetParent(int elementId);
        TomatoView GetTomato();
        int GetRootRequest(int elementId, string path, string userName);
        TomatoReportView GetTomatoReport(DateTime date);
    }

    public class TaskQueries : ITaskQueries
    {
        private readonly IMapper _mapper;
        ITaskRepository _taskRepository;
        ITomatoRepository tomatoRepository;

        public TaskQueries(ITaskRepository taskRepository, ITomatoRepository tomatoRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            this.tomatoRepository = tomatoRepository;
            _mapper = mapper;
        }


        public ElementView GetTaskList(int? bagId, string path, string userName)
        {
            var r = GetTaskListInternal(bagId, path, userName, SearchConditions.GetTodaysList);
            return r;
        }

        public ElementView GetTaskListFinishedThisWeek(int? bagId, string path, string userName)
        {
            var r = GetTaskListInternal(bagId, path, userName, SearchConditions.GetFinshedThisWeek);
            return r;
        }

        public ElementView GetTaskListFinishedLast7Days(int? bagId, string path, string userName)
        {
            var r = GetTaskListInternal(bagId, path, userName, SearchConditions.GetFinshedLast7Days);
            return r;
        }

        //pw:change it to handlers
        private ElementView GetTaskListInternal(int? bagId, string path, string userName, string searchConditions)
        {
            if (bagId.HasValue && !string.IsNullOrEmpty(path))
            {
                bagId = FindCorrectRoot(bagId.Value, path, userName);
            }
            else if (bagId.HasValue == false)
            {
                //get root of the user
                var userRootElement = _taskRepository.GetRootForUser(userName);
                bagId = userRootElement.ElementId;
            }
            else if(bagId.HasValue && string.IsNullOrEmpty(path))
            {

            }

            Infrastructure.Element element = _taskRepository.GetStructure(searchConditions, bagId.Value, userName);
            ElementView st = _mapper.Map<Infrastructure.Element, ElementView>(element);
            return st;
        }

        //public ElementView GetTaskListFinishedThisWeek(int? bagId, string path, string userName)
        //{

        //    if (bagId.HasValue && !string.IsNullOrEmpty(path))
        //    {
        //        bagId = FindCorrectRoot(bagId.Value, path, userName);
        //    }
        //    else if (bagId.HasValue == false)
        //    {
        //        //get root of the user
        //        var userRootElement = _taskRepository.GetRootForUser(userName);
        //        bagId = userRootElement.ElementId;
        //    }
        //    else if (bagId.HasValue && string.IsNullOrEmpty(path))
        //    {

        //    }

        //    Infrastructure.Element element = _taskRepository.GetStructure(SearchConditions.GetFinshedThisWeek, bagId.Value, userName);
        //    ElementView st = _mapper.Map<Infrastructure.Element, ElementView>(element);
        //    return st;
        //}

        public int GetRootRequest(int elementId, string path, string userName)
        {
            var result = FindCorrectRoot(elementId, path, userName);
            return result;
        }

        private int FindCorrectRoot(int elementId, string path, string userName)
        {
            if (path.StartsWith("\\"))
            {
                var r = FindAbsolute(path);
                return r;
            }
            if (!path.StartsWith("\\"))
            {
                var r = FindRelative(elementId, path, userName);
                return r;
            }

            throw new Exception("This won't happen");
        }

        private int FindRelative(int bagId, string path,string userName)
        {
            var element = _taskRepository.GetNode(SearchConditions.GetTodaysList, bagId, userName);
            string[] parts = path.Split("\\");
            foreach (var part in parts)
            {
                if (part == "..")
                {
                    element = _taskRepository.GetNode(SearchConditions.GetTodaysList, element.ParentId.Value,userName);
                }
                else
                {
                    //pw: add returning errors;
                    element = element.Elements.SingleOrDefault(x => x.Name == part);
                    element = _taskRepository.GetNode(SearchConditions.GetTodaysList, element.ElementId, userName);
                }
            }

            return element.ElementId;
        }

        private int FindAbsolute(string path)
        {
            throw new NotImplementedException();
        }

        public int? GetParent(int elementId)
        {
            var element = _taskRepository.Get(elementId);
            return element?.ParentId;
        }

        public TomatoView GetTomato()
        {
            var tomato = tomatoRepository.GetCurrent();
            TomatoView result = _mapper.Map<Infrastructure.Tomato, TomatoView>(tomato);
            return result;
        }

        public TomatoReportView GetTomatoReport(DateTime date)
        {
            List<Infrastructure.Tomato> tomatoList = tomatoRepository.GetTomatoReport(date);
            var result = new TomatoReportView();
            result.Tomatoes = _mapper.Map<List<Infrastructure.Tomato>, List<TomatoView>>(tomatoList);
            return result;
        }
    }
}
