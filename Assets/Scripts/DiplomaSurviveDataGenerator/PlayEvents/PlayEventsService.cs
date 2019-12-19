using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class PlayEventsService : IPlayEventsService
    {
        private readonly BaseContext _context;
        private readonly INumberDistribution _generator;
        private readonly IPlayEventStore _eventsService;
        public double NextTime { get; private set; } = 0;

        public PlayEventsService(BaseContext context, INumberDistribution generator, IPlayEventStore eventService)
        {
            _context = context ?? throw new ArgumentNullException();
            _generator = generator ?? throw new ArgumentNullException();
            _eventsService = eventService ?? throw new ArgumentNullException();
        }

        public void GenerateNextTime()
        {
            NextTime = _context.Time.Current + _generator.Next();
        }

        public PlayEvent GetEvent()
        {
            return _eventsService.Get(_context);
        }
    }
}
