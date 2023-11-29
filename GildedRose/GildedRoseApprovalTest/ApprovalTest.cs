using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using GildedRoseKata;

namespace GildedRoseApprovalTest;


[UseReporter(typeof(DiffReporter))]
public class ApprovalTest
{
    private readonly List<Item> _items = new()
    {
        new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
        new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
        new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
        new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
        new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
        new Item
        {
            Name = "Backstage passes to a TAFKAL80ETC concert",
            SellIn = 15,
            Quality = 20
        },
        new Item
        {
            Name = "Backstage passes to a TAFKAL80ETC concert",
            SellIn = 10,
            Quality = 49
        },
        new Item
        {
            Name = "Backstage passes to a TAFKAL80ETC concert",
            SellIn = 5,
            Quality = 49
        },
        new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
    };
    
    [Test]
    public void ThirtyDays()
    {
        var stringBuilder = new StringBuilder();
        var app = new GildedRose(_items);
        
        for (var i = 0; i < 30; i++)
        {
            stringBuilder.AppendLine($"-------- day {i} --------");
            stringBuilder.AppendLine("name, sellIn, quality");
            foreach (var item in _items)
            {
                stringBuilder.AppendLine($"{item.Name}, {item.SellIn}, {item.Quality}");
            }
            stringBuilder.AppendLine();
            app.UpdateQuality();
        }
        Approvals.Verify(stringBuilder);
    }
}