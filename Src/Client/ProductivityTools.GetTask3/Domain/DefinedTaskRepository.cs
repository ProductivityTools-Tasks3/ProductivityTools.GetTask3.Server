﻿using ProductivityTools.GetTask3.Client;
using ProductivityTools.GetTask3.Contract.Requests;
using ProductivityTools.GetTask3.Contract.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Domain
{
    class DefinedTaskRepository
    {
        public DefinedTaskView Get()
        {
            var rootElement = GetTaskHttpClient.Post2<DefinedTaskView>("DefinedTask", "List", new ListDefinedTaskRequest()).Result;
            return rootElement;
        }

        public DefinedTaskView GetWithDetails()
        {
            var rootElement = GetTaskHttpClient.Post2<DefinedTaskView>("DefinedTask", "List", new ListDefinedTaskRequest() { IncudeDetails = true }).Result;
            return rootElement;
        }

        public DefinedTaskView GetForBag(int? bagId)
        {
            var rootElement = GetTaskHttpClient.Post2<DefinedTaskView>("DefinedTask", "List", new ListDefinedTaskRequest() { BagId = bagId, IncudeDetails = true }).Result;
            return rootElement;
        }

        public DefinedTaskView GetWithDetailsForbagId(int? bagId)
        {
            var rootElement = GetTaskHttpClient.Post2<DefinedTaskView>("DefinedTask", "List", new ListDefinedTaskRequest() { BagId = bagId, IncudeDetails = true }).Result;
            return rootElement;
        }

        public DefinedTaskGroupView GetDefinedTaskGroup(string name)
        {
            var result = GetTaskHttpClient.Post2<DefinedTaskGroupView>("DefinedTask", "GetDefinedTaskGroupView", new GetDefinedTaskGroupRequest() { DefinedTaskGroupName = name }).Result;
            return result;
        }

        public void AddDefinedTasks(int definedTaskId)
        {
            var rootElement = GetTaskHttpClient.Post2<DefinedTaskView>("DefinedTask", "AddDefinedTasks", new AddDefinedTasksRequest() { DefinedTaskId = definedTaskId }).Result;
        }
    }
}
