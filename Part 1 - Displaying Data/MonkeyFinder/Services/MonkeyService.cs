using System.Net.Http.Json;// This is to access the ReadFromJsonAsync method

namespace MonkeyFinder.Services;

public class MonkeyService
{

    // Creating a new instance of HTTP client
    HttpClient httpClient;
    public MonkeyService()
    {
        this.httpClient = new HttpClient();
    }


    // An array list of monkeys 
    List<Monkey> monkeyList = new();



    // Creates a new list of monkey's and return it 
    public async Task<List<Monkey>> GetMonkeys()
    {
        if (monkeyList?.Count > 0) {
            return monkeyList;
        } else {

            // Perform Network Operation to fetch monkeys from endPoint
            var response = await httpClient.GetAsync("https://www.montemagno.com/monkeys.json");

            // Checks if the network operation was successful
            if (response.IsSuccessStatusCode) {
                // decoding monkey list from data received from endPoint
                monkeyList = await response.Content.ReadFromJsonAsync(MonkeyContext.Default.ListMonkey);

            }

            return monkeyList;
        }

    }







}
