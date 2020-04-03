using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Fakes.Tests
{
    public class TomatoRepositoryTest : ITomatoRepository
    {
        public Tomato CurrentTestTomato { get; set; }

        public void Add(Tomato entity)
        {
            throw new NotImplementedException();
        }

        public Tomato Get(int? id)
        {
            throw new NotImplementedException();
        }

        public Tomato GetCurrent()
        {
            return CurrentTestTomato;
        }

        public List<Tomato> GetTomatoReport(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void Update(Tomato entity)
        {
            throw new NotImplementedException();
        }
    }
}
