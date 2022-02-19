using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Fakes.Tests
{
    public class TomatoRepositoryTest : ITomatoRepository
    {
        public Infrastructure.Tomato CurrentTestTomato { get; set; }

        public void Add(Infrastructure.Tomato entity)
        {
            throw new NotImplementedException();
        }

        public Infrastructure.Tomato Get(int? id)
        {
            throw new NotImplementedException();
        }

        public Infrastructure.Tomato GetCurrent()
        {
            return CurrentTestTomato;
        }

        public List<Infrastructure.Tomato> GetTomatoReport(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void Update(Infrastructure.Tomato entity)
        {
            throw new NotImplementedException();
        }
    }
}
