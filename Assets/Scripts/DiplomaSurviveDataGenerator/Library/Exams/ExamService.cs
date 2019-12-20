using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class ExamService : IExamService
    {
        private readonly BaseContext _context;
        private readonly INumberGenerator _generator;
        private readonly IStore<IExam> _examStore;
        public double NextTime { get; private set; } = 0;

        public ExamService(BaseContext context, INumberGenerator generator, IStore<IExam> examStore)
        {
            _context = context ?? throw new ArgumentNullException();
            _generator = generator ?? throw new ArgumentNullException();
            _examStore = examStore ?? throw new ArgumentNullException();
        }

        public IExam GetSession(int? level = null)
        {
            return GetExam(ExamType.Session, level);
        }

        public IExam GetEIT(int? level = null)
        {
            if (!level.HasValue)
            {
                level = _context.Main.Level;
            }
            return GetExam(ExamType.EIT, level);
        }

        private IExam GetExam(ExamType type, int? level)
        {
            var exams = _examStore
                .GetAll()
                .Where(ex => (ex.Type == type || ex.Type == ExamType.Universal) 
                        && (level == null || ex.Level <= 0 || ex.Level == level))
                .ToList();

            if (exams.Count == 0)
            {
                return null;
            }

            return exams[_generator.Next(exams.Count)];
        }
    }
}
