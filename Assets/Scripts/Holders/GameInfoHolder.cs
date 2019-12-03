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
        
        private readonly IAppStateHandler _appStateHandler;
        private readonly ILocalDataProvider _localDataProvider;

        public GameInfoHolder(IAppStateHandler appStateHandler,
            ILocalDataProvider localDataProvider)
        {
            _localDataProvider = localDataProvider;
            _appStateHandler = appStateHandler;
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

           _gameInfoDto = dto;
        }

        private GameInfoDto CreateGameInfo()
        {
            var dto = new GameInfoDto();
            return dto;
        }
    }
}