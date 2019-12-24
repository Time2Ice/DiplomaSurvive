using DataProvider;
using Game;
using System;

namespace DefaultNamespace
{
    public class PlayerInfoHolder : IPlayerInfoHolder
    {

        public event Action<int, int> PersonalLifeChanged;

        public event Action<int> CoinsChanged;

        public event Action<int> CourseChanged;

        public event Action<int> MaxCourseChanged;
        public event Action<int> UniversityChanged;
        public event Action<int> MaxPrivateLifeChanged;
        public event Action<int> PointsChanged;
        public event Action<int> ChangedPosition;

        public bool IsClassroom { get; set; } = false;


        private PlayerInfoDto _playerInfoDto;

        public int[] Abilities
        {

            get => _playerInfoDto.abilities;
            set
            {
                if (_playerInfoDto.AbilitiesEqual(value))
                    return;

                _playerInfoDto.abilities = value;
                _localDataProvider.Save(_playerInfoDto);
            }
        }

        public int[] Courses
        {

            get => _playerInfoDto.courses;
            set
            {
                if (_playerInfoDto.CoursesEqual(value))
                    return;

                _playerInfoDto.courses = value;
                _localDataProvider.Save(_playerInfoDto);
            }
        }

        public string[] Reasons
        {

            get => _playerInfoDto.reasons;
            set
            {
                if (_playerInfoDto.ReasonsEqual(value))
                    return;

                _playerInfoDto.reasons = value;
                _localDataProvider.Save(_playerInfoDto);
            }
        }

        public int MaxCourse
        {

            get => _playerInfoDto.max_course;
            set
            {
                if (_playerInfoDto.max_course == value)
                    return;

                _playerInfoDto.max_course = value;
                _localDataProvider.Save(_playerInfoDto);
                MaxCourseChanged?.Invoke(_playerInfoDto.max_course);
            }
        }

        public int University
        {
            get => _playerInfoDto.university;
            set
            {
                if (_playerInfoDto.university == value)
                    return;

                _playerInfoDto.university = value;
                _localDataProvider.Save(_playerInfoDto);
                UniversityChanged?.Invoke(_playerInfoDto.university);
            }
        }
        public int Coins
        {
            get => _playerInfoDto.coins;
            set
            {
                if (_playerInfoDto.coins == value)
                    return;

                _playerInfoDto.coins = value;
                _localDataProvider.Save(_playerInfoDto);
                CoinsChanged?.Invoke(_playerInfoDto.coins);
            }
        }

        public int Points
        {
            get => _playerInfoDto.points;
            set
            {
                if (_playerInfoDto.points == value)
                    return;

                _playerInfoDto.points = value;
                _localDataProvider.Save(_playerInfoDto);
                PointsChanged?.Invoke(value);
            }
        }
        public int MaxPoints
        {
            get => _playerInfoDto.max_points;
            set
            {
                if (_playerInfoDto.max_points == value)
                    return;

                _playerInfoDto.max_points = value;
                _localDataProvider.Save(_playerInfoDto);
                _requests.SetScore();
            }
        }
        public int UniversityPoints
        {
            get => _playerInfoDto.university_points;
            set
            {
                if (_playerInfoDto.university_points == value)
                    return;

                _playerInfoDto.university_points = value;
                if (_playerInfoDto.university_points > MaxPoints)
                    MaxPoints = _playerInfoDto.university_points;
                _localDataProvider.Save(_playerInfoDto);
            }
        }

        public int TasksTaken
        {
            get => _playerInfoDto.tasks_taken;
            set
            {
                if (_playerInfoDto.tasks_taken == value)
                    return;

                _playerInfoDto.tasks_taken = value;
                _localDataProvider.Save(_playerInfoDto);
            }
        }

        public int TaskQueueCapasity
        {
            get => _playerInfoDto.task_queue_capacity;
            set
            {
                if (_playerInfoDto.task_queue_capacity == value)
                    return;

                _playerInfoDto.task_queue_capacity = value;
                _localDataProvider.Save(_playerInfoDto);
            }
        }

        public int PrivateLife
        {

            get => _playerInfoDto.private_life;
            set
            {
                if (_playerInfoDto.private_life == value)
                    return;

                _playerInfoDto.private_life = value;
                _localDataProvider.Save(_playerInfoDto);
                PersonalLifeChanged?.Invoke(_playerInfoDto.private_life, _playerInfoDto.max_private_life);
            }
        }
        public int MaxPrivateLife
        {

            get => _playerInfoDto.max_private_life;
            set
            {
                if (_playerInfoDto.max_private_life == value)
                    return;

                _playerInfoDto.max_private_life = value;
                _localDataProvider.Save(_playerInfoDto);
                PersonalLifeChanged?.Invoke(_playerInfoDto.private_life, _playerInfoDto.max_private_life);
                MaxPrivateLifeChanged?.Invoke(_playerInfoDto.max_private_life);
            }
        }
        public int CurrentCourse
        {

            get => _playerInfoDto.current_course;
            set
            {
                if (_playerInfoDto.current_course == value)
                    return;

                _playerInfoDto.current_course = value;
                _localDataProvider.Save(_playerInfoDto);
                if (_playerInfoDto.current_course > _playerInfoDto.max_course)
                {
                    MaxCourse = _playerInfoDto.current_course;
                }
                CourseChanged?.Invoke(_playerInfoDto.current_course);
            }
        }

        public int PossibilityToStay
        {

            get => _playerInfoDto.possibility_to_stay;
            set
            {
                if (_playerInfoDto.possibility_to_stay == value)
                    return;

                _playerInfoDto.possibility_to_stay = value;
                _localDataProvider.Save(_playerInfoDto);
            }
        }

        public string Token
        {

            get => _playerInfoDto.token;
            set
            {
                if (_playerInfoDto.token == value)
                    return;

                _playerInfoDto.token = value;
                _localDataProvider.Save(_playerInfoDto);
            }
        }

        public string Name
        {

            get => _playerInfoDto.name;
            set
            {
                if (_playerInfoDto.name == value)
                    return;

                _playerInfoDto.name = value;
                _localDataProvider.Save(_playerInfoDto);
            }
        }

        public int Position
        {

            get => _playerInfoDto.position;
            set
            {
                if (_playerInfoDto.position == value)
                    return;

                _playerInfoDto.position = value;
                _localDataProvider.Save(_playerInfoDto);
                ChangedPosition?.Invoke(_playerInfoDto.position);
            }
        }

        private readonly IAppStateHandler _appStateHandler;
        private readonly ILocalDataProvider _localDataProvider;
        private Requests _requests;

        public PlayerInfoHolder(Requests requests, IAppStateHandler appStateHandler,
            ILocalDataProvider localDataProvider)
        {
            _localDataProvider = localDataProvider;
            _appStateHandler = appStateHandler;
           _requests = requests;
            requests.PlayerInfoHolder = this;
            SetData();

        }

        private void SetData()
        {
            if (!_appStateHandler.GetData(out PlayerInfoDto dto))
            {
                if (_localDataProvider.Exist<PlayerInfoDto>())
                {
                    dto = _localDataProvider.Load<PlayerInfoDto>();
                    _playerInfoDto = dto;
                    _requests.Authenticate();
                }
                else
                {
                    dto = CreatePlayerInfo();
                    _playerInfoDto = dto;
                    _localDataProvider.Save(dto);
                    _requests.SignInRequest();
                }
            }

            //var dto = CreatePlayerInfo();
            //_playerInfoDto = dto;
            //_requests.SignInRequest();
        }

        private PlayerInfoDto CreatePlayerInfo()
        {
            var dto = new PlayerInfoDto()
            {
                coins = 600,
                points = 0,
                private_life = 30,
                max_private_life = 30,
                current_course = 0,
                max_course = 0,
                task_queue_capacity = 20,
                abilities = new int[0],
                courses = new int[0],
                reasons = new string[0] ,
                name = System.Guid.NewGuid().ToString()
            };
            return dto;
        }
    }
}