using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tim_reader_v2
{
   
    public class MetaAndTXTObjects
    {
        public override string ToString()
        {
            if (MetadataVersion == null)
            {
                if (PlayerCount == 1)
                {
                    return $"{Utilities.MapNameConv(LevelName)} | Solo | Round: {CurrentRound.Number} TimSaved";
                }
                else if (PlayerCount != 1)
                {
                    return $"{Utilities.MapNameConv(LevelName)} | {PlayerCount} player | Round: {CurrentRound.Number} TimSaved";
                }
                return $"{Utilities.MapNameConv(LevelName)} | Round: {CurrentRound.Number} TimSaved";
            }
            else
            {
                if (PlayerCount == 1)
                {
                    return $"{Utilities.MapNameConv(LevelName)} | Solo | Round: {CurrentRound.Number} MetaInfo";
                }
                else if (PlayerCount != 1)
                {
                    return $"{Utilities.MapNameConv(LevelName)} | {PlayerCount} player | Round: {CurrentRound.Number} MetaInfo";
                }
                return $"{Utilities.MapNameConv(LevelName)} | Round: {CurrentRound.Number} MetaInfo";
            }
        }

        [JsonProperty("LevelName")]
        public string LevelName { get; set; }

        [JsonProperty("StartTimeDate")]
        public string StartTimeDate { get; set; }

        [JsonProperty("EndTimeDate")]
        public string EndTimeDate { get; set; }

        [JsonProperty("StartTimeDateUTC")]
        public string StartTimeDateUtc { get; set; }

        [JsonProperty("EndTimeDateUTC")]
        public string EndTimeDateUtc { get; set; }

        [JsonProperty("StartTimestamp")]
        public long StartTimestamp { get; set; }

        [JsonProperty("EndTimestamp")]
        public long EndTimestamp { get; set; }

        [JsonProperty("SetUpTimestamp")]
        public long SetUpTimestamp { get; set; }

        [JsonProperty("SetUpBoxHits")]
        public long SetUpBoxHits { get; set; }

        [JsonProperty("PlayerCount")]
        public uint PlayerCount { get; set; }

        [JsonProperty("NMLEndTimestamp")]
        public long? NmlEndTimestamp { get; set; }

        [JsonProperty("NMLKills")]
        public long? NmlKills { get; set; }

        [JsonProperty("DropsGrabbed")]
        public long DropsGrabbed { get; set; }

        [JsonProperty("PowerOnTimestamp")]
        public long PowerOnTimestamp { get; set; }

        [JsonProperty("BoxHits")]
        public long BoxHits { get; set; }

        [JsonProperty("CurrentBoxHits")]
        public long CurrentBoxHits { get; set; }

        [JsonProperty("BoxHitsRespins")]
        public long BoxHitsRespins { get; set; }

        [JsonProperty("TrapHits")]
        public long TrapHits { get; set; }

        [JsonProperty("SoloLivesGiven")]
        public long SoloLivesGiven { get; set; }

        [JsonProperty("Resetted")]
        public bool Resetted { get; set; }

        [JsonProperty("Rounds")]
        public Round[] Rounds { get; set; }

        [JsonProperty("CurrentRound")]
        public Round CurrentRound { get; set; }

        [JsonProperty("Trades")]
        public Trade[] Trades { get; set; }

        [JsonProperty("CurrentTrade")]
        public Trade CurrentTrade { get; set; }

        [JsonProperty("Ready")]
        public bool Ready { get; set; }

        //meta stuff 
        [JsonProperty("MetadataVersion")]
        public string MetadataVersion { get; set; }

        [JsonProperty("FH")]
        public string Fh { get; set; }

        [JsonProperty("DWSId")]
        public long? DwsId { get; set; }

        [JsonProperty("TIMMSMT")]
        public long? Timmsmt { get; set; }

        [JsonProperty("StartUnixTimestamp")]
        public long? StartUnixTimestamp { get; set; }

        [JsonProperty("EndUnixTimestamp")]
        public long? EndUnixTimestamp { get; set; }

        [JsonProperty("Players")]
        public Player[] Players { get; set; }

        [JsonProperty("EndSource")]
        public string EndSource { get; set; }

        [JsonProperty("PerkBroughtRound")]
        public long PerkBroughtRound { get; set; }

        [JsonProperty("PowerOnRound")]
        public long PowerOnRound { get; set; }

        [JsonProperty("FirstRoom")]
        public bool FirstRoom { get; set; }

        [JsonProperty("PerkActivity")]
        public Activity[] PerkActivity { get; set; }

        [JsonProperty("WeaponActivity")]
        public Activity[] WeaponActivity { get; set; }

        [JsonProperty("TotalTradeTime")]
        public long TotalTradeTime { get; set; }
    }
    public class Trade
    {
        [JsonProperty("StartTimestamp")]
        public long StartTimestamp { get; set; }

        [JsonProperty("EndTimestamp")]
        public long EndTimestamp { get; set; }

        [JsonProperty("StartRoundNumber")]
        public long StartRoundNumber { get; set; }

        [JsonProperty("EndRoundNumber")]
        public long EndRoundNumber { get; set; }

        [JsonProperty("BoxHits")]
        public long BoxHits { get; set; }

        [JsonProperty("Time")]
        public long Time { get; set; }

        [JsonProperty("TrapHits")]
        public long TrapHits { get; set; }
    }
    public class Round
    {
        [JsonProperty("Number")]
        public long Number { get; set; }

        [JsonProperty("StartTimestamp")]
        public long StartTimestamp { get; set; }

        [JsonProperty("EndTimestamp")]
        public long EndTimestamp { get; set; }

        [JsonProperty("BoxHits")]
        public long BoxHits { get; set; }

        [JsonProperty("TrapHits")]
        public long TrapHits { get; set; }

        [JsonProperty("DropsGrabbed")]
        public long DropsGrabbed { get; set; }

        [JsonProperty("IsSpecialRound")]
        public bool IsSpecialRound { get; set; }

        [JsonProperty("MissedInsta")]
        public bool MissedInsta { get; set; }
        //meta stuff
        [JsonProperty("PlayerStats")]
        public PlayerStat[] PlayerStats { get; set; }
    }
    public class PlayerStat
    {
        [JsonProperty("Slot")]
        public long Slot { get; set; }

        [JsonProperty("Downs")]
        public long Downs { get; set; }

        [JsonProperty("Kills")]
        public long Kills { get; set; }

        [JsonProperty("Headshots")]
        public long Headshots { get; set; }

        [JsonProperty("Points")]
        public long Points { get; set; }
    }
    public class Activity
    {
        [JsonProperty("Timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("PlayerSlot")]
        public long PlayerSlot { get; set; }

        [JsonProperty("Event")]
        public Event Event { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }
    public class Player
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("XUID")]
        public double Xuid { get; set; }

        [JsonProperty("Slot")]
        public long Slot { get; set; }

        [JsonProperty("Downs")]
        public long Downs { get; set; }

        [JsonProperty("Kills")]
        public long Kills { get; set; }

        [JsonProperty("Headshots")]
        public long Headshots { get; set; }

        [JsonProperty("Points")]
        public long Points { get; set; }
    }
    public enum Event { Add, Lost };
    internal class EventConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Event) || t == typeof(Event?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "add":
                    return Event.Add;
                case "lost":
                    return Event.Lost;
            }
            throw new Exception("Cannot unmarshal type Event");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Event)untypedValue;
            switch (value)
            {
                case Event.Add:
                    serializer.Serialize(writer, "add");
                    return;
                case Event.Lost:
                    serializer.Serialize(writer, "lost");
                    return;
            }
            throw new Exception("Cannot marshal type Event");
        }

        public static readonly EventConverter Singleton = new EventConverter();
    }
}
