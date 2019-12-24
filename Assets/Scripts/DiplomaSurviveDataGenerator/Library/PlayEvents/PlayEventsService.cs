using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class PlayEventsService : IPlayEventsService
    {
        private readonly IPlayContext _context;
        private readonly INumberGenerator _generator;
        private readonly IPlayEventStore _eventsService;
        
        public double EventProbability { get; set; } = 0.0009;

        public bool IsAvailable
        {
            get
            {
                return EventProbability >= _generator.NextDouble01();
            }
        }

        public PlayEventsService(IPlayContext context, INumberGenerator generator, IPlayEventStore eventService)
        {
            _context = context;
            _generator = generator ?? new DefaultNumberGenerator();
            _eventsService = eventService ?? throw new ArgumentNullException();
        }

        public PlayEvent GetEvent()
        {
            return _eventsService.Get(_context);
        }
    }
}
