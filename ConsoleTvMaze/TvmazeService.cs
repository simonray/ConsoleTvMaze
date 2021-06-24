using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class TvmazeService
{
    public async Task<List<Root>> Go(IList<string> favorites)
    {
        string content;
        string fn = $"tvmaze-{DateTime.Now.ToString("yyyy-MM-dd")}.json";

        if (File.Exists(fn))
        {
            Console.WriteLine("Getting schedule (cached)");
            content = await File.ReadAllTextAsync(fn);
        }
        else
        {
            var files = Directory.GetFiles(".", "tvmaze-*.json", SearchOption.TopDirectoryOnly);
            if (files.Length > 0)
            {
                Console.WriteLine($"Deleting cached files");
                foreach(var file in files) File.Delete(file);
            }

            var _client = new HttpClient();
            Console.WriteLine("Getting schedule (http)");
            var response = await _client.GetAsync($"http://api.tvmaze.com/schedule/full");
            content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Caching schedule to file {fn}");
            await File.WriteAllTextAsync(fn, content);
        }

        var root = JsonSerializer.Deserialize<List<Root>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        foreach(var programme in root.Where(p => favorites
                .Any(n => p._embedded.show.name.RemovePunctuation()
                    .Contains(n.RemovePunctuation(), StringComparison.OrdinalIgnoreCase)))
            )
        {
            ConsoleColor colour = ConsoleColor.DarkGray;
            if (DateTime.Parse(programme.airdate).Date < DateTime.Now.Date)
                colour = ConsoleColor.DarkGreen;
            else if (DateTime.Parse(programme.airdate).Date == DateTime.Now.Date)
                colour = ConsoleColor.Green;
            else if (Episode(programme) == 0 || Episode(programme) == 1)
                colour = ConsoleColor.DarkMagenta;
            ColorConsole.WriteLine(GetProgramme(programme), colour);
        }

        return root;
    }

    private string GetProgramme(Root programme)
    {
        var name = programme._embedded.show.name;
        var startTime = programme.airstamp.ToShortDateString();
        var duration = programme.runtime ?? 0;
        var endTime = programme.airstamp.AddMinutes(duration);
        var title = programme.name;
        var season = $"S{programme.season.ToString("D2")}E{Episode(programme).ToString("D2")}";
        var network = programme._embedded.show.network.name;
        
        return $"{startTime} {name}: {title} {season} ({duration}) {network}";
    }

    private int Episode(Root programme)
        => programme.number.HasValue ? programme.number.Value : 0;
}
