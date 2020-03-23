using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetEsport
{
    public partial class Form1 : Form
    {
        public static string createDate(DateTime dateTime)
        {
            try
            {
                if (dateTime == null)
                {
                    return "Non déterminée";
                }
                return dateTime.ToString();
            }
            catch (FormatException)
            {
                return "Parsing failed!";
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonTournois_Click(object sender, EventArgs e)
        {
            panelMenuTournois.Visible = true;
            panelAffichageTournois.Visible = true;
        }

        private void buttonMatchs_Click(object sender, EventArgs e)
        {
            panelMenuTournois.Visible = false;
            panelAffichageTournois.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonTournoisRunning_Click(object sender, EventArgs e)
        {
            WebRequest request = HttpWebRequest.Create("https://api.pandascore.co/csgo/tournaments/running?sort=being_at&token=Z6NMfMOR_sgphX2aFt2PkJD1elVs6k7BGULPuj3WXpz2v__zd-4");
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string RunningTournament_JSON = reader.ReadToEnd();

            RunningTournament[] myRunningTournanement = Newtonsoft.Json.JsonConvert.DeserializeObject<RunningTournament[]>(RunningTournament_JSON);
        }

        private void buttonTournoisUpcoming_Click(object sender, EventArgs e)
        {
            WebRequest request = HttpWebRequest.Create("https://api.pandascore.co/csgo/tournaments/upcoming?sort=begin_at&token=Z6NMfMOR_sgphX2aFt2PkJD1elVs6k7BGULPuj3WXpz2v__zd-4");
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string UpcomingTournament_JSON = reader.ReadToEnd();

            UpcomingTournament[] myUpcomingTournanement = Newtonsoft.Json.JsonConvert.DeserializeObject<UpcomingTournament[]>(UpcomingTournament_JSON);

            listViewListeTournois.Items.Clear();

            foreach (UpcomingTournament tournament in myUpcomingTournanement)
            {
                string[] ligneSerieTn = new string[] { "placeholder", tournament.serie.full_name, tournament.league.name , createDate(tournament.begin_at), createDate(tournament.end_at) };
                var lvi = new ListViewItem(ligneSerieTn);
                lvi.Tag = ligneSerieTn;
                listViewListeTournois.Items.Add(lvi);
            }
           
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
public class League
{
    public int id { get; set; }
    public string image_url { get; set; }
    public DateTime modified_at { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
    public string url { get; set; }
}

public class Live
{
    public object opens_at { get; set; }
    public bool supported { get; set; }
    public object url { get; set; }
}

public class Match
{
    public DateTime? begin_at { get; set; }
    public bool detailed_stats { get; set; }
    public bool draw { get; set; }
    public DateTime? end_at { get; set; }
    public bool forfeit { get; set; }
    public object game_advantage { get; set; }
    public int id { get; set; }
    public Live live { get; set; }
    public string live_url { get; set; }
    public string match_type { get; set; }
    public DateTime modified_at { get; set; }
    public string name { get; set; }
    public int number_of_games { get; set; }
    public DateTime original_scheduled_at { get; set; }
    public bool rescheduled { get; set; }
    public DateTime scheduled_at { get; set; }
    public string slug { get; set; }
    public string status { get; set; }
    public int tournament_id { get; set; }
    public int? winner_id { get; set; }
}

public class Serie
{
    public DateTime begin_at { get; set; }
    public object description { get; set; }
    public DateTime? end_at { get; set; }
    public string full_name { get; set; }
    public int id { get; set; }
    public int league_id { get; set; }
    public DateTime modified_at { get; set; }
    public string name { get; set; }
    public string season { get; set; }
    public string slug { get; set; }
    public object winner_id { get; set; }
    public object winner_type { get; set; }
    public int year { get; set; }
}

public class Team
{
    public string acronym { get; set; }
    public int id { get; set; }
    public string image_url { get; set; }
    public string location { get; set; }
    public DateTime modified_at { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
}

public class Videogame
{
    public int id { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
}

public class RunningTournament
{
    public DateTime begin_at { get; set; }
    public DateTime end_at { get; set; }
    public int id { get; set; }
    public League league { get; set; }
    public int league_id { get; set; }
    public bool live_supported { get; set; }
    public List<Match> matches { get; set; }
    public DateTime modified_at { get; set; }
    public string name { get; set; }
    public string prizepool { get; set; }
    public Serie serie { get; set; }
    public int serie_id { get; set; }
    public string slug { get; set; }
    public List<Team> teams { get; set; }
    public Videogame videogame { get; set; }
    public object winner_id { get; set; }
    public object winner_type { get; set; }
}
public class UpcomingTournament
{
    public DateTime begin_at { get; set; }
    public DateTime? end_at { get; set; }
    public int id { get; set; }
    public League league { get; set; }
    public int league_id { get; set; }
    public bool live_supported { get; set; }
    public List<Match> matches { get; set; }
    public DateTime modified_at { get; set; }
    public string name { get; set; }
    public string prizepool { get; set; }
    public Serie serie { get; set; }
    public int serie_id { get; set; }
    public string slug { get; set; }
    public List<object> teams { get; set; }
    public Videogame videogame { get; set; }
    public object winner_id { get; set; }
    public object winner_type { get; set; }
}
