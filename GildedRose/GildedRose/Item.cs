namespace GildedRoseKata;

public class Item
{
    public string Name { get; init; }
    public int SellIn { get; set; }
    public int Quality { get; set; }

    private bool IsAgedBrie => Name == "Aged Brie";
    private bool IsBackstagePasses => Name == "Backstage passes to a TAFKAL80ETC concert";
    private bool IsSulfuras => Name == "Sulfuras, Hand of Ragnaros";
    private bool IsConjured => Name == "Conjured Mana Cake";

    public void Update()
    {
        if (IsAgedBrie)
        {
            UpdateAgedBrie();
        }
        else if (IsBackstagePasses)
        {
            UpdateBackstagePasses();
        }
        else if (IsSulfuras)
        {
            // Do nothing
        }
        else if (IsConjured)
        {
            UpdateQuality(updateBy: 2);
        }
        else
        {
            UpdateQuality();
        }
    }

    private void UpdateQuality(int updateBy = 1)
    {
        if (Quality > 0)
        {
            Quality -= updateBy;
        }

        SellIn -= 1;

        if (SellIn < 0 && Quality > 0)
        {
            Quality -= updateBy;
        }
    }

    private void UpdateBackstagePasses()
    {
        if (Quality < 50)
        {
            Quality += 1;

            if (SellIn < 11 && Quality < 50)
            {
                Quality += 1;
            }

            if (SellIn < 6 && Quality < 50)
            {
                Quality += 1;
            }
        }

        SellIn -= 1;

        if (SellIn < 0)
        {
            Quality -= Quality;
        }
    }

    private void UpdateAgedBrie()
    {
        if (Quality < 50)
        {
            Quality += 1;
        }

        SellIn -= 1;

        if (SellIn < 0 && Quality < 50)
        {
            Quality += 1;
        }
    }
}