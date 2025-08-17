using Raylib_cs;
using static Raylib_cs.KeyboardKey;
using static Raylib_cs.Raylib;
using System.Numerics;

InitWindow(1500, 1500, "Hello, World!");

SetConfigFlags(ConfigFlags.Msaa4xHint);

var player_position = new Vector2(
        Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);
var player_speed = new Vector2(5, 4);
int player_radius = 20;

var screen_center = new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2);
var container_radius = 200;

List<(Vector2 pos, Vector2 vel)> balls = [];

var spawn_num_max = 1000;
var spawn_num = 5;
var spawn_radius = 5;

var random = new Random();

Raylib.SetTargetFPS(120);

while(!WindowShouldClose()) {
    if(IsKeyPressed(Q)) Environment.Exit(1);

    player_position += player_speed;

    for(int i = 0; i < balls.Count; i++) {
        balls[i] = (balls[i].pos + balls[i].vel, balls[i].vel);

        var ball_offset = balls[i].pos - screen_center;
        var ball_dist = ball_offset.Length();

        if(ball_dist + spawn_radius > container_radius) {
            var normal = Vector2.Normalize(ball_offset);
            balls[i] = (balls[i].pos, balls[i].vel - 2 * Vector2.Dot(balls[i].vel, normal) * normal);
            balls[i] = (screen_center + normal * (container_radius - spawn_radius), balls[i].vel);
        }
    }

    var offset = player_position - screen_center;
    var dist = offset.Length();

    if(dist + player_radius > container_radius) {
        var normal = Vector2.Normalize(offset);
        player_speed = player_speed - 2 * Vector2.Dot(player_speed, normal) * normal;
        player_position = screen_center + normal * (container_radius - player_radius);
    }

    spawn_ball();

    BeginDrawing();

    ClearBackground(Color.White);

    DrawCircleLines((int)screen_center.X, (int)screen_center.Y, container_radius, Color.Black);
    DrawCircleV(player_position, (float)player_radius, Color.Maroon);

    foreach(var ball in balls) {
        DrawCircleV(ball.pos, 10, Color.Pink);
    }

    DrawFPS(10, 10);

    EndDrawing();
}

CloseWindow();

void spawn_ball() {
    var num = random.Next(0, spawn_num_max);

    if (spawn_num < num) return;

    balls.Add((screen_center,
                new Vector2(random.Next(-10, 10), random.Next(-10, 10))));
}
