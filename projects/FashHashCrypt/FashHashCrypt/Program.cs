List<string> passwords = [
    "uzWC67",
    "2L6KZG",
    "JlFRCZ",
    "8ntwUp",
    "YOyhfR",
    "RSbvHv",
    "rwVDh9",
    "LdNyos",
    "gynQMT",
    "AtnDjr"
];

List<string> hashes1 = [
    "$2a$11$4jBOGdDUf3K6YymyXaijXufRIjSuBfsR0f8h3v6IGVohnLM9LZVO6",
    "$2a$11$TkYOoEqSOidmxM/9Mi3bS.mZh.SOWYEbzqPnW9XyJFpaaycaXFktW",
    "$2a$11$VPvRzWDOYgKu4rlUtlwMsuj2C9CF/WmzmArlBtlWhDERY4nCC3xAi",
    "$2a$11$fjGAE0NWrBISQXh1r7MQT.PKru4w05eYfzAqELnbhGjXXLgACq0Pi",
    "$2a$11$l7OtDQeGSioXpAgz/SsGhuD7o5T1VTTFS1TkEKyX5kX0KVxvf6XC2",
    "$2a$11$LTaVL0FHxa9tDxQbhp7NU.qTMnOeudJze8.Yh1VXLbZyc0oFF257G",
    "$2a$11$0RqU3TSfziVWGQ9Qb3ewkO8REvMw7ARj3HBPngPcGr/FZ60TeDHAq",
    "$2a$11$6kLNVqXqz27BpyCJTsdoGeiB.0h0wyfooYyDcAxtXcZKAErwvSVS6",
    "$2a$11$aykH/It1r7.WjMI5XbdR..FKMeRqiMeGnWiiJvZSjVNlhofspqyti",
    "$2a$11$aLOfbqA0Qj/e0s8RdaDyeu7jk3biJhn2bB3i2.dKMxivle0p1RzeW"
];

List<string> hashes = new();

//foreach (string p in passwords)
//    hashes.Add(HashPassword(p));

//foreach (string h in hashes)
//    Console.WriteLine($"{h}");

for (int i = 0; i < hashes1.Count; i++)
{
    if (VerifyPassword(passwords[i], hashes1[i]))
        Console.WriteLine(1);
    else
        Console.WriteLine(0);
}

static string HashPassword(string password)
    => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

static bool VerifyPassword(string password, string passwordHash)
    => BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);