namespace Test01;
public class Human
{
    public string FistName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    private string? _sex;
    public string? Sex
    {
        get
        {
            return _sex;
        }
        set
        {
            _sex = value;
        }
    }
    public int? Age { get; set; }
    public string Name
    {
        get
        {
            return LastName + " " + FistName;
        }
    }

    private string? _nickName;
    public string? NickName
    {
        get => _nickName;
        set => _nickName = value;
    }
}