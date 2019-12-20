using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class CheckLastClickTime : BaseCheckStep
    {
        public double DeductionProbabilityMin { get; set; } = 0;
        public double DeductionProbabilityMax { get; set; } = 0;
        public double NextCheck { get; set; }
        public double MinTimeAfterClick { get; set; } = -1;
        public double MaxTimeAfterClick { get; set; } = double.MaxValue;

        public CheckLastClickTime(IPlayContext context) : base(context)
        {
           // _context.Time.OnLastClickChanged += AskForCheck;
        }
     /*   protected override bool TryHandle(ref double probability)
        {
            if (_context.Time.Current <= MinTimeAfterClick || _context.Time.Current >= MaxTimeAfterClick)
            {
                return false;
            }
            else if (_context.Time.Current > MinTimeAfterClick)
            {
                probability = DeductionProbabilityMin;
                return true;
            }
            else
            {
                probability = DeductionProbabilityMax;
                return true;
            }
        */
    }
}
