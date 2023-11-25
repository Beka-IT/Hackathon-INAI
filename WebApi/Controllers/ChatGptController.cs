using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ChatGptController : ControllerBase
{
    private Dictionary<int, (string name, string street)> map = new Dictionary<int, (string, string)>()
    {
        {1, new ("Головной офис","Московская, 80/1")},
        {2, new ("Центр денежных переводов","Улица Фрунзе, 338")},
        {3, new ("Сберкасса № 033-0-01","Московская 185")},
        {4, new ("Октябрьский филиал","Чуй 48")},
    };

    private Dictionary<int, string> operationMap = new Dictionary<int, string>()
    {
        { 1, "Перевод" },
        { 2, "Оплата" },
        { 3, "Кредит" },
        { 4, "Открытие счета" },
    };
    
    private static readonly Regex regex = new Regex(@"\d+");
    static string ExtractFirstNumber(string text)
    {
        var match = regex.Match(text);
        return match.Success ? match.Value : "0";
    }
    [HttpPost]
    public async Task<ActionResult<int>> PostMessage(PostMessageArgument argument)
    {
        var openAiApiClient = new OpenAIApiClient();
        var branchOper = openAiApiClient.SendPrompt(branchText + argument.Message);
        var timeOper = openAiApiClient.SendPrompt(timeText + argument.Message);
        var OperOper = openAiApiClient.SendPrompt(OperationText + argument.Message);
        int branchId = 0;
        int operationType = 0;
        // await Task.WhenAll(branchOper, timeOper, OperOper);
        var message = string.Empty;
        try
        {
            var branchTextResponse = GetFromGPT(await branchOper);
            branchId = int.Parse(ExtractFirstNumber(branchTextResponse));
            var operationTypeTextResponse = GetFromGPT(await OperOper);
            operationType = int.Parse(ExtractFirstNumber(operationTypeTextResponse));
        }
        catch(HttpRequestException ex)
        {
            message = "Много запросов\n";
        }
        catch(Exception ex)
        {
            message = "Не удалось спарсить данные\n";
        }
        
        
        DateTime startDate = default;
        try
        {
            var timeText = GetFromGPT(await timeOper);
            var time = timeText.Split("\n").Last();
            startDate = DateTime.Parse(time);
        }
        catch
        {
            message += "Не найдена информация о времени\n";
        }
        if (branchId == 0)
        {
            message += "Не найден филиал\n";
        }

        if (operationType == 0)
        {
            message += "Не найден тип операции\n";
        }

        return string.IsNullOrWhiteSpace(message) ? Ok(
            $@"
              Филиал: {map[branchId].name}, {map[branchId].street},
              Операция: {operationMap[operationType]},
              Время: {startDate.ToString()}
            ") : BadRequest(message);
    }

    private string GetFromGPT(string text)
    {
        return JsonSerializer.Deserialize<ChatGptResponse>(text)?.Choices.FirstOrDefault()?.Text;
    }

    private string branchText = @"Oпредели к какой из нижеследующих улиц есть связь у высказывания и возрати в ответ только цифру, если совпадения незначительные возвращай 0:
     id:1, name: Московская 80
     id:2, name: Улица Фрунзе 338
     id:3, name: Московская 185
     id:4, name: Чуй 48
     высказывание:
    ";
    
    private string OperationText = @"
    Есть четыре вариантов ответа:
    1) Перевод
    2) Оплата
    3) Кредит
    4) Открытие счета

Найди связанный ответ на вопрос, ответь только цифрой без текста, если не найдешь подходящего ответа верни только цифру 0 без текста.
вопрос:
    ";

    private string timeText = @"
    определи время(time) из текста  и если нет года то текущий год вставляй и ответ  только в формате  ISO 8601 без лишнего текста ,если не найдешь подходящего ответа верни только 0 без текста.
текст:
";
}