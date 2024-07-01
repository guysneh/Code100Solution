using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

int centerXleft = 250;
int centerYleft = 150;
int innerRadiusLeft = 55;
int outerRadiusLeft = 75;

int centerXright = 410;
int centerYright = 150;
int innerRadiusRight = 55;
int outerRadiusRight = 75;

int rectTopLeftX = 145;
int rectTopLeftY = 75;
int rectBottomRightX = 165;
int rectBottomRightY = 225;

int result = 0;
int[][] coords = ConvertJsonToArray("coordinatesystem.json");

foreach (int[] point in coords) 
{
    int pointX = point[0];
    int pointY = point[1];
    bool pointIsInLogo = IsPointBetweenCircles(centerXleft, centerYleft, innerRadiusLeft, outerRadiusLeft, pointX, pointY) ||
     IsPointBetweenCircles(centerXright, centerYright, innerRadiusRight, outerRadiusRight, pointX, pointY) ||
     IsPointInRectangle(rectTopLeftX, rectTopLeftY, rectBottomRightX, rectBottomRightY, pointX, pointY);
    if (pointIsInLogo) 
    {
        result++;
    }
}
return result;

static int[][] ConvertJsonToArray(string filePath) 
{
    // Read the JSON file into a string
    string json = File.ReadAllText(filePath);

    // Deserialize the JSON into a dynamic object
    var jsonObject = JsonConvert.DeserializeObject<dynamic>(json);

    // Extract the coordinates
    var coords = jsonObject["coords"].ToObject<int[][]>();

    return coords;
}

static bool IsPointBetweenCircles(int centerX, int centerY, int innerRadius, int outerRadius, int pointX, int pointY)
{
    // Calculate the distance from the point to the center of the circles
    double distance = Math.Sqrt(Math.Pow(pointX - centerX, 2) + Math.Pow(pointY - centerY, 2));

    // Check if the distance is between the inner and outer radii
    return distance >= innerRadius && distance <= outerRadius;
}
static bool IsPointInRectangle(int rectTopLeftX, int rectTopLeftY, int rectBottomRightX, int rectBottomRightY, int pointX, int pointY)
{
    return pointX >= rectTopLeftX &&
           pointX <= rectBottomRightX &&
           pointY >= rectTopLeftY &&
           pointY <= rectBottomRightY;
}
