using Assets.Scripts.Handlers;
using DataProvider;
using DiplomaSurviveDataGenerator;
using Game;
using Zenject;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameInfoHolder : IGameInfoHolder
    {
        private GameInfoDto _gameInfoDto;

        public AbilityDto[] Abilities => _gameInfoDto.abilities;
        public CourseDto[] Courses => _gameInfoDto.courses;
        public ReasonDto[] Reasons => _gameInfoDto.reasons;

        public int TaskCompletePoints => _gameInfoDto.task_points;

        public int TaskCompleteCoins => _gameInfoDto.task_coins;

        public int ReasonPossibilityPlus => _gameInfoDto.reason_possibility_plus;
        public int[] TaskTimes => _gameInfoDto.task_times;
        public int PrivateLifeClickReduce => _gameInfoDto.personal_life_click_reduce;
        public float PersonalLifeIncreaseTime => _gameInfoDto.personal_life_increase_time;

        public PlayState CurrentState => _playStateHandler.GetState();

        private readonly IAppStateHandler _appStateHandler;
        private readonly ILocalDataProvider _localDataProvider;
        private readonly IPlayStateHandler _playStateHandler;
        private readonly IStore<Exclusion> _exclusionStore;

        public GameInfoHolder(IAppStateHandler appStateHandler,
            IPlayStateHandler playStateHandler,
            ILocalDataProvider localDataProvider, IStore<Exclusion> exclusionStore)
        {
            _localDataProvider = localDataProvider;
            _appStateHandler = appStateHandler;
            _playStateHandler = playStateHandler;
            _exclusionStore = exclusionStore;
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
            //else
            //{
                dto = CreateGameInfo();
            //}
            _gameInfoDto = dto;
        }

        private GameInfoDto CreateGameInfo()
        {
            var exclusions = _exclusionStore.GetAll();

            var dto = new GameInfoDto()
            {
                task_points = 3,
                task_coins = 30,
                task_times = new[] { 6, 6 },
                personal_life_click_reduce = 3,
                personal_life_increase_time = 2,
                courses = new[]
                {
                    new CourseDto { number = 1, points_to_next = 40 },
                    new CourseDto { number = 2, points_to_next = 40 }
                },
                abilities = new AbilityDto[]
                {
                    new AbilityDto() {id = 1, name = "Increase max private life by 20", price = 300},
                    new AbilityDto() {id = 2, name = "Increase tasks capacity by 10", price = 300},
                },
                reasons = new ReasonDto[exclusions.Count]
            };

            for(int i = 0; i < exclusions.Count; i++)
            {
                var exclusion = exclusions[i];
                dto.reasons[i] = new ReasonDto
                {
                    Description = exclusion.Title,
                    Name = exclusion.Description,
                    Id = exclusion.Id
                };
            }

            return dto;
        }
    }
}