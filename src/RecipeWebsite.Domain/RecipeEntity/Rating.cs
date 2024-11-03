namespace RecipeWebsite.Domain.RecipeEntity;

public class Rating
{
    public int Rate => ComputeRate();
    public int TotalRates { get; set; }
    public int TotalVotes { get; set; }
    
    private int ComputeRate()
    {
        return TotalVotes == 0 ? 0 : (int)Math.Round((float)TotalRates / TotalVotes);
    }

    public void AddRate(Stars star)
    {
        TotalRates += (int)star;
        TotalVotes++;
    }
}