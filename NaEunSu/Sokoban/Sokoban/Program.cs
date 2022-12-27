// 초기 세팅
Console.ResetColor(); // 컬러를 초기화한다
Console.CursorVisible = false; // 커서를 숨긴다
Console.Title = "소코반"; // 타이틀을 설정한다
Console.BackgroundColor = ConsoleColor.DarkYellow; // 배경색을 설정한다
Console.ForegroundColor = ConsoleColor.White; // 글꼴색을 설정한다
Console.Clear(); // 출력된 모든 내용을 지운다

// 게임 루프
// 플레이어 좌표 설정
int playerX = 0;
int playerY = 0;

int boxX = 5;
int boxY = 5;

// 게임 루프 == 프레임
// 가로 15 세로 10
while (true)
{
    Console.Clear(); // 이전 프레임을 지운다
    // ------------------------------ Render -------------------------
    // 플레이어 출력하기


    Console.SetCursorPosition(playerX, playerY);
    Console.Write("N");

    Console.SetCursorPosition(boxX, boxY);
    Console.Write("■");


    // ------------------------------ ProcessInput ----------------------

    ConsoleKey key = Console.ReadKey().Key;

    // --------------------------- Update ------------------------------
    // 오른쪽 화살표키를 눌렀을 때
    if (key == ConsoleKey.RightArrow)
    {
        playerX = Math.Min(playerX + 1, 15); // 오른쪽으로 이동 
        boxX = Math.Min(boxX , 14);
    }

    if (key == ConsoleKey.LeftArrow)
    {
        playerX = Math.Max(0, playerX - 1); // 왼쪽으로 이동
        boxX = Math.Max(0, boxX - 2);
    }

    if (key == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(0, playerY - 1); ; // 위쪽으로 이동
        boxY = Math.Max(0, boxY - 2);
    }

    if (key == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY + 1, 10); // 아래쪽으로 이동
        boxY = Math.Min(boxY, 9);
    }
    



}




