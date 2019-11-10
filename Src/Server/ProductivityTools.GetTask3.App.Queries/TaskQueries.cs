using AutoMapper;
using ProductivityTools.GetTask3.Contract;
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
        ElementView GetTaskList(int? bagId, string path);
        int? GetParent(int elementId);
        TomatoView GetTomato();
        int? GetRootRequest(int? elementId, string path);
    }

    public class TaskQueries : ITaskQueries
    {
        private readonly IMapper _mapper;
        ITaskRepository _taskRepository;
        ITomatoRepository _tomatoRepository;

        public TaskQueries(ITaskRepository taskRepository, ITomatoRepository tomatoRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _tomatoRepository = tomatoRepository;
            _mapper = mapper;
        }

        //pw:change it to handlers
        public ElementView GetTaskList(int? bagId, string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                bagId = FindCorrectRoot(bagId, path);
            }

            Domain.Element element = _taskRepository.GetStructure(bagId);
            ElementView st = _mapper.Map<Domain.Element, ElementView>(element);
            return st;
        }

        public int? GetRootRequest(int? elementId, string path)
        {
            var result = FindCorrectRoot(elementId, path);
            return result;
        }

        private int? FindCorrectRoot(int? elementId, string path)
        {
            if (path.StartsWith("\\"))
            {
                var r = FindAbsolute(path);
                return r;
            }
            if (!path.StartsWith("\\"))
            {
                var r = FindRelative(elementId, path);
                return r;
            }

            throw new Exception("This won't happen");
        }

        private int FindRelative(int? bagId, string path)
        {
            var element = _taskRepository.GetNode(bagId);
            string[] parts = path.Split("\\");
            foreach (var part in parts)
            {
                if (part == "..")
                {
                    element = _taskRepository.GetNode(element.ParentId);
                }
                else
                {
                    //pw: add returning errors;
                    element = element.Elements.SingleOrDefault(x => x.Name == part);
                    element = _taskRepository.GetNode(element.ElementId);
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
            var tomato = _tomatoRepository.GetCurrent();
            TomatoView result = _mapper.Map<Domain.Tomato, TomatoView>(tomato);
            return result;
        }


    }
}
