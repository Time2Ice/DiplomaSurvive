using Assets.Scripts.Handlers;
using DataProvider;
using Game;
using Zenject;

namespace DefaultNamespace
{
    public class GameInfoHolder : IGameInfoHolder
    {
        private GameInfoDto _gameInfoDto;
        
        public AbilityDto[] Abilities=>_gameInfoDto.abilities;
        public CourseDto[] Courses=>_gameInfoDto.courses;
        public ReasonCategory[] ReasonCategories=>_gameInfoDto.reason_categories;

        public int TaskPrice=>_gameInfoDto.task_price;
        public int ReasonPossibilityPlus=>_gameInfoDto.reason_possibility_plus;
        public int[] TaskTimes=>_gameInfoDto.task_times;
        public int PrivateLifeClickReduce => _gameInfoDto.personal_life_click_reduce;
        public float PersonalLifeIncreaseTime => _gameInfoDto.personal_life_increase_time;

        public PlayState CurrentState => _playStateHandler.GetState();

        private readonly IAppStateHandler _appStateHandler;
        private readonly ILocalDataProvider _localDataProvider;
        private readonly IPlayStateHandler _playStateHandler;

        public GameInfoHolder(IAppStateHandler appStateHandler, IPlayStateHandler playStateHandler,
            ILocalDataProvider localDataProvider)
        {
            _localDataProvider = localDataProvider;
            _appStateHandler = appStateHandler;
            _playStateHandler = playStateHandler;
            SetData();
        }
        
        private void SetData()
        {
           if (!_appStateHandler.GetData(out GameInfoDto dto))
            {
                if (_localDataProvider.Exist<GameInfoDto>())
                {
                    dto = _localDataProvider.Load<GameInfoDto>();
                }
                else
                {
                    dto = CreateGameInfo();
                    _localDataProvider.Save(dto);
                }
            }
            dto = CreateGameInfo();
            _gameInfoDto = dto;
        }

        private GameInfoDto CreateGameInfo()
        {
            var dto = new GameInfoDto()
            {
                task_price = 3,
                task_times = new[] { 6, 6 },
                personal_life_click_reduce = 3,
                personal_life_increase_time = 2,
                courses = new[] { new CourseDto { number = 1, points_to_next = 40 }, new CourseDto { number = 2, points_to_next = 40 } }
            };
            return dto;
        }
    }
}