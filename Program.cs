using Raylib_cs;
using static Raylib_cs.KeyboardKey;
using static Raylib_cs.Raylib;
using System.Numerics;

InitWindow(1500, 1500, "Hello, World!");

SetConfigFlags(ConfigFlags.Msaa4xHint);

var ball_position = new Vector2( Raylib.GetScreenWidth() / 2,
        Raylib.GetScreenHeight() / 2);

var ball_speed = new Vector2(5, 4);

var screen_center = new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2);

var container_radius = 200;

int ball_radius = 20;
bool pause = false;
int frame_count = 0;

Raylib.SetTargetFPS(120);


while(!WindowShouldClose()) {

    if(IsKeyPressed(Space)) pause = !pause;
    if(IsKeyPressed(Q)) Environment.Exit(1);

    if(!pause) {
        ball_position += ball_speed;

        var offset = ball_position - screen_center;
        var dist = offset.Length();

        if(dist + ball_radius > container_radius) {
            var normal = Vector2.Normalize(offset);
            ball_speed = ball_speed - 2 * Vector2.Dot(ball_speed, normal) * normal;
            ball_position = screen_center + normal * (container_radius - ball_radius);
        }
    }
    else frame_count++;

    BeginDrawing();

    ClearBackground(Color.White);

    DrawCircleLines((int)screen_center.X, (int)screen_center.Y, container_radius, Color.Black);
    DrawCircleV(ball_position, (float)ball_radius, Color.Maroon);


    DrawFPS(10, 10);


    EndDrawing();
}

CloseWindow();
