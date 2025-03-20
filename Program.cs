using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace Day7
{
    internal class Program
    {
        struct Position //각종 위치 구조체
        {
            public int x;
            public int y;

        }






        static void Main(string[] args) // 메인 함수
        {
            Console.Clear();
            bool gameOver = true;
            Position player;
            char[,] map;
            Start(out player, out map);
            Console.CursorVisible = false;

            while (gameOver == true) // 게임오버가 true일 동안 반복 동작
            {
                Console.SetCursorPosition(0, 0); // 맵 플레이어 초기화
                Show(map);
                Player(player);
                ConsoleKey key = Input();
                Move(key, ref player, map);
                Finish(ref gameOver, map, player);
            }


            if (gameOver == false)
            {
                End();

            }

            // Conosle clear와 같은 기능으로  이동 후 새로 그리기
            // 2차ㅣ원 배열에 들은 요소에 따라 이동 가능, 불가능 판단
            // 게임적인 메커니즘을 추가하여 목표를 달성하도록 함.



            static void Start(out Position player, out char[,] map) // 초기 시작
            {

                Console.Write("아무키를 입력해주세요.");
                Console.ReadKey(true);

                player.x = 1;
                player.y = 1;

                Map(out map);
                Console.Clear();
            }

            static void Show(char[,] map) // 맵 출력
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    for (int x = 0; x < map.GetLength(1); x++)
                    {
                        Console.Write(map[y, x]);
                    }
                    Console.WriteLine();
                }
            }

            static void Move(ConsoleKey key, ref Position player, char[,] map) //입력 을 받아 이동 
            {
                Position position;
                Position target;
                Position goal;
                goal.x = 2;
                goal.y = 15;
                switch (key)
                {

                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        position.x = player.x - 1;
                        position.y = player.y;
                        target.x = player.x - 2;
                        target.y = player.y;
                        break;

                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        position.x = player.x + 1;
                        position.y = player.y;
                        target.x = player.x + 2;
                        target.y = player.y;

                        break;

                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        position.x = player.x;
                        position.y = player.y - 1;
                        target.x = player.x;
                        target.y = player.y - 2;

                        break;


                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        position.x = player.x;
                        position.y = player.y + 1;
                        target.x = player.x;
                        target.y = player.y + 2;

                        break;

                    default:
                        return;
                }

                if (map[position.y, position.x] != '#')  // 앞이 벽이 아닐 때
                {
                    if (map[position.y, position.x] != '*') // 앞이 장애물이 아닐 때
                    {
                        map[position.y, position.x] = ' '; // 앞 칸을 빈칸으로 만들어줌.
                        player.x = position.x; // 플레이어 이동
                        player.y = position.y; // 플레이어 이동
                    }
                    else
                    {
                        map[position.y, position.x] = '*'; // 그 외에는 앞에 장애물 생성.
                    }

                }


                if (map[position.y, position.x] == '*') // 앞에 장애물일 때
                {
                    if (map[target.y, target.x] != '#') // 장애물 앞에 벽이 아닐 때
                    {
                        map[target.y, target.x] = '*'; // 장애물 앞에 장애물 생성.
                        map[position.y, position.x] = ' '; // 장애물있던 자리 즉, 앞 칸을 빈칸으로 만듬.
                        player.x = position.x; // 플레이어 이동.
                        player.y = position.y; // 플레이어 이동.
                    }

                }

            }

            static ConsoleKey Input() //입력
            {
                return Console.ReadKey(true).Key;
            }


            static bool Goal(char[,] map, Position player) // 목표 설정.
            {
                int count = 0;
                foreach (char tile in map)
                {
                    if (tile == '$') // 달러가 있으면 카운트 추가.
                    {
                        count++;
                    }
                    if (tile == '☆') // 별이 있으면 카운트 추가.
                    {
                        count++;
                    }
                }

                if (count == 2) // 둘 다 있으면 카운트가 2라서 true
                {
                    return true;
                }
                else if (count == 0) 
                    return false;
                else
                    return true;

            }

            static void Finish(ref bool gameOver, char[,] map, Position player) // 조건 달성시 함수
            {
                bool finish = Goal(map, player);
                if (!finish)
                {
                    gameOver = false;
                }
            }

            static void Map(out char[,] map) // 맵
            {
                map = new char[16, 16]
                {
                   { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                   { '#', ' ', '#', '#', ' ', ' ', '#', ' ', '#', '#', ' ', '*', ' ', ' ', ' ', '#'},
                   { '#', '*', '#', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', '#', '#', ' ', '#', '#' },
                   { '#', ' ', '*', ' ', ' ', '#', '#', ' ', ' ', '#', ' ', '#', ' ', '*', '#', '#' },
                   { '#', ' ', '#', ' ', ' ', '#', ' ', ' ', '#', '#', ' ', '#', ' ', ' ', ' ', '#' },
                   { '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#' },
                   { '#', '#', '#', '#', '#', '#', '#', ' ', '#', '#', '#', '#', '#', ' ', '$', '#' },
                   { '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#' },
                   { '#', '#', '#', ' ', '#', ' ', ' ', ' ', '#', '#', ' ', ' ', ' ', ' ', ' ', '#' },
                   { '#', ' ', ' ', ' ', '#', '#', '#', ' ', ' ', '#', ' ', ' ', '#', '#', ' ', '#' },
                   { '#', ' ', '#', '#', '#', ' ', ' ', ' ', ' ', '#', '#', ' ', '#', ' ', ' ', '#' },
                   { '#', ' ', '#', ' ', '#', ' ', ' ', '#', ' ', ' ', '#', ' ', '#', ' ', '#', '#' },
                   { '#', '#', '#', ' ', '#', '#', ' ', '#', ' ', '#', '#', ' ', '#', ' ', ' ', '#' },
                   { '#', ' ', ' ', '*', ' ', '#', '#', '#', ' ', '#', ' ', ' ', '#', '#', '#', '#' },
                   { '#', '☆', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#' },
                   { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' }
                };

            }

            static void Player(Position player) // 플레이어 출력
            {
                Console.SetCursorPosition(player.x, player.y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("▼");
                Console.ResetColor();
            }



            static void End() // 마지막 출력
            {
                Console.Clear();
                Console.WriteLine("축하합니다!!!");
            }



        }
    }
}
