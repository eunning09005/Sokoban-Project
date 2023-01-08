using System;
using System.Reflection;
using System.Runtime.Serialization.Formatters;

namespace _01._05
{
    // 열거형
    enum Direction // 방향을 저장하는 타입 멤버의 타입은 보통 int(왜냐하면 실수는 언제나 부정확하기 때문에)
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    class Program
    {
        static void Main()
        {
            // 초기 세팅
            Console.ResetColor(); // 컬러를 초기화하는 것
            Console.CursorVisible = false; // 커서를 숨기기
            Console.Title = "소코반게임"; // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.DarkMagenta; // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Yellow; // 글꼴색을 설정한다.
            Console.Clear(); // 출력된 내용을 지워준다.

            // 중간중간에 디버그하여 오류 확인 필수!!

            // 기호 상수 정의
            const int GOAL_COUNT = 2;
            const int BOX_COUNT = GOAL_COUNT;
            const int WALL_COUNT = GOAL_COUNT;

            // 플레이어 위치를 저장하기 위한 변수
            int playerX = 0;
            int playerY = 0;

            // 플레이어의 이동 방향을 저장하기 위한 변수
            Direction playerMoveDirection = Direction.None;

            // 플레이어가 무슨 박스를 밀고 있는지 저장하기 위한 변수

            // 박스의 위치를 저장하기 위한 변수
            int[] boxpositionsX = { 5, 7 };
            int[] boxpositionsY = { 6, 9 };

            // 벽의 위치를 저장하기 위한 변수
            int[] wallpositonsX = { 6, 9 };
            int[] wallpositionsY = { 12, 3 };

            // 골인의 위치를 저장하기 위한 변수

            int[] goalpositionsX = { 19, 17 };
            int[] goalpositionsY = { 5, 8 };

            // 밀고 있는 박스를 저장하기 위한 변수
            int pushedBoxId = 0;

            // 게임 루프 구성
            while (true)
            {
                // ------------------------------Render-----------------------------------
                // 이전 프레임을 지운다.
                Console.Clear();

                // 플레이어를 그린다.
                Console.SetCursorPosition(playerX, playerY);
                Console.Write("P");

                // 벽을 그린다.
                for (int i = 0; i < WALL_COUNT; ++i)
                {
                    int wallX = wallpositonsX[i];
                    int wallY = wallpositionsY[i];

                    Console.SetCursorPosition(wallX, wallY);
                    Console.Write("W");
                }

                // 골을 그린다.
                for (int i = 0; i < GOAL_COUNT; ++i)
                {
                    int goalX = goalpositionsX[i];
                    int goalY = goalpositionsY[i];

                    Console.SetCursorPosition(goalX, goalY);
                    Console.Write("G");
                }

                // 박스를 그린다.
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    int boxX = boxpositionsX[i];
                    int boxY = boxpositionsY[i];

                    Console.SetCursorPosition(boxX, boxY);
                    Console.Write("B");

                    for (int j = 0; j < GOAL_COUNT; ++j)
                    {
                        int goalX = goalpositionsX[j];
                        int goalY = goalpositionsY[j];

                        if (boxX == goalX && boxY == goalY)
                        {
                            Console.SetCursorPosition(boxX, boxY);
                            Console.Write("★");
                        }
                    }
                }

                // ------------------------------ProcessInput-----------------------------
                ConsoleKey key = Console.ReadKey().Key; // 열거형은 내부 멤버의 타입을 따른다.
                // ------------------------------Update-----------------------------------

                // 플레이어 이동 처리
                if (key == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(0, playerX - 1);
                    playerMoveDirection = Direction.Left;
                }

                if (key == ConsoleKey.RightArrow)
                {
                    playerX = Math.Min(20, playerX + 1);
                    playerMoveDirection = Direction.Right;
                }

                if (key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(15, playerY + 1);
                    playerMoveDirection = Direction.Down;
                }

                if (key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(0, playerY - 1);
                    playerMoveDirection = Direction.Up;
                }

                // 플레이어와 벽의 충돌 처리
                for (int WallId = 0; WallId < WALL_COUNT; ++WallId)
                {
                    int wallX = wallpositonsX[WallId];
                    int wallY = wallpositionsY[WallId];

                    if (playerX == wallX && playerY == wallY)
                    {
                        switch (playerMoveDirection)
                        {
                            case Direction.Left:
                                playerX = wallX + 1;
                                break;

                            case Direction.Right:
                                playerX = wallX - 1;
                                break;

                            case Direction.Up:
                                playerY = wallY + 1;
                                break;

                            case Direction.Down:
                                playerY = wallY - 1;
                                break;
                        }
                    }
                }

                // 박스 이동 처리
                // 플레이어가 박스를 밀었을 때라는 게 무엇을 의미하는가? => 플레이어가 이동했을 때 플레이어의 위치와 박스 위치가 겹쳤다.
                for (int BoxId = 0; BoxId < BOX_COUNT; ++BoxId)
                {
                    int boxX = boxpositionsX[BoxId];
                    int boxY = boxpositionsY[BoxId];

                    if (playerX == boxX && playerY == boxY)
                    {
                        // 박스를 밀면 된다. => 박스의 좌표를 바꾼다.
                        switch (playerMoveDirection)
                        {
                            case Direction.Left:
                                boxX = Math.Max(0, boxX - 1);
                                playerX = boxX + 1;
                                break;

                            case Direction.Right:
                                boxX = Math.Min(boxX + 1, 20);
                                playerX = boxX - 1;
                                break;

                            case Direction.Up:
                                boxY = Math.Max(boxY - 1, 0);
                                playerY = boxY + 1;
                                break;

                            case Direction.Down:
                                boxY = Math.Min(boxY + 1, 15);
                                playerY = boxY - 1;
                                break;

                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");

                                return;
                        }

                        boxpositionsX[BoxId] = boxX;
                        boxpositionsY[BoxId] = boxY;

                        pushedBoxId = BoxId;
                    }
                }

                // 벽과 박스가 부딪혔을 때
                for (int WallId = 0; WallId < WALL_COUNT; ++WallId)
                {
                    int wallX = wallpositonsX[WallId];
                    int wallY = wallpositionsY[WallId];

                    for (int BoxId = 0; BoxId < BOX_COUNT; ++BoxId)
                    {
                        int boxX = boxpositionsX[BoxId];
                        int boxY = boxpositionsY[BoxId];

                        if (boxX == wallX && boxY == wallY)
                        {
                            switch (playerMoveDirection)
                            {
                                case Direction.Left:
                                    boxX = wallX + 1;
                                    playerX = boxX + 1;
                                    break;

                                case Direction.Right:
                                    boxX = wallX - 1;
                                    playerX = boxX - 1;
                                    break;

                                case Direction.Up:
                                    boxY = wallY + 1;
                                    playerY = boxY + 1;
                                    break;

                                case Direction.Down:
                                    boxY = wallY - 1;
                                    playerY = wallY - 1;
                                    break;
                            }

                            boxpositionsX[BoxId] = boxX;
                            boxpositionsY[BoxId] = boxY;
                        }
                    }
                }

                // 박스끼리 충돌처리
                for (int collidedBoxId = 0; collidedBoxId < BOX_COUNT; ++collidedBoxId) // 박스 1을 밀어서 박스2에 닿은 건지, 박스2을 열어서 박스 1에 닿은건지?
                {
                    if (pushedBoxId == collidedBoxId)
                    {
                        continue;
                    }

                    if (boxpositionsX[pushedBoxId] == boxpositionsX[collidedBoxId] && boxpositionsY[pushedBoxId] == boxpositionsY[collidedBoxId])
                    {
                        switch (playerMoveDirection)
                        {
                            case Direction.Left:
                                boxpositionsX[pushedBoxId] = boxpositionsX[collidedBoxId] + 1;
                                playerX = boxpositionsX[pushedBoxId] + 1;
                                break;

                            case Direction.Right:
                                boxpositionsX[pushedBoxId] = boxpositionsX[collidedBoxId] - 1;
                                playerX = boxpositionsX[pushedBoxId] - 1;
                                break;

                            case Direction.Up:
                                boxpositionsY[pushedBoxId] = boxpositionsY[collidedBoxId] + 1;
                                playerY = boxpositionsY[pushedBoxId] + 1;
                                break;

                            case Direction.Down:
                                boxpositionsY[pushedBoxId] = boxpositionsY[collidedBoxId] - 1;
                                playerY = boxpositionsY[pushedBoxId] - 1;
                                break;
                        }
                    }
                }

                // 박스의 위치와 골의 위치가 같을때
                // 모든 골 지점에 박스가 올라와 있다.
                int count = 0;

                for (int i = 0; i < GOAL_COUNT; ++i)
                {
                    int goalX = goalpositionsX[i];
                    int goalY = goalpositionsY[i];

                    for (int j = 0; j < BOX_COUNT; ++j)
                    {
                        int boxX = boxpositionsX[j];
                        int boxY = boxpositionsY[j];

                        if (boxX == goalX && boxY == goalY)
                        {
                            ++count;

                            if (count == 2)
                            {
                                Console.Clear();
                                Console.WriteLine("클리어~! 축하합니다!");

                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}




