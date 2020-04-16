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
using System.Windows.Forms.VisualStyles;

namespace ProjetEsport
{
    public partial class Form1 : Form
    {
        public class ListViewExtender : IDisposable
        {
            private readonly Dictionary<int, ListViewColumn> _columns = new Dictionary<int, ListViewColumn>();

            public ListViewExtender(ListView listView)
            {
                if (listView == null)
                    throw new ArgumentNullException("listView");

                if (listView.View != View.Details)
                    throw new ArgumentException(null, "listView");

                ListView = listView;
                ListView.OwnerDraw = true;
                ListView.DrawItem += OnDrawItem;
                ListView.DrawSubItem += OnDrawSubItem;
                ListView.DrawColumnHeader += OnDrawColumnHeader;
                ListView.MouseMove += OnMouseMove;
                ListView.MouseClick += OnMouseClick;

                Font = new Font(ListView.Font.FontFamily, ListView.Font.Size - 2);
            }

            public virtual Font Font { get; private set; }
            public ListView ListView { get; private set; }

            protected virtual void OnMouseClick(object sender, MouseEventArgs e)
            {
                ListViewItem item;
                ListViewItem.ListViewSubItem sub;
                ListViewColumn column = GetColumnAt(e.X, e.Y, out item, out sub);
                if (column != null)
                {
                    column.MouseClick(e, item, sub);
                }
            }

            public ListViewColumn GetColumnAt(int x, int y, out ListViewItem item, out ListViewItem.ListViewSubItem subItem)
            {
                subItem = null;
                item = ListView.GetItemAt(x, y);
                if (item == null)
                    return null;

                subItem = item.GetSubItemAt(x, y);
                if (subItem == null)
                    return null;

                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    if (item.SubItems[i] == subItem)
                        return GetColumn(i);
                }
                return null;
            }

            protected virtual void OnMouseMove(object sender, MouseEventArgs e)
            {
                ListViewItem item;
                ListViewItem.ListViewSubItem sub;
                ListViewColumn column = GetColumnAt(e.X, e.Y, out item, out sub);
                if (column != null)
                {
                    column.Invalidate(item, sub);
                    return;
                }
                if (item != null)
                {
                    ListView.Invalidate(item.Bounds);
                }
            }

            protected virtual void OnDrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
            {
                e.DrawDefault = true;
            }

            protected virtual void OnDrawSubItem(object sender, DrawListViewSubItemEventArgs e)
            {
                ListViewColumn column = GetColumn(e.ColumnIndex);
                if (column == null)
                {
                    e.DrawDefault = true;
                    return;
                }

                column.Draw(e);
            }

            protected virtual void OnDrawItem(object sender, DrawListViewItemEventArgs e)
            {
                // do nothing
            }

            public void AddColumn(ListViewColumn column)
            {
                if (column == null)
                    throw new ArgumentNullException("column");

                column.Extender = this;
                _columns[column.ColumnIndex] = column;
            }

            public ListViewColumn GetColumn(int index)
            {
                ListViewColumn column;
                return _columns.TryGetValue(index, out column) ? column : null;
            }

            public IEnumerable<ListViewColumn> Columns
            {
                get
                {
                    return _columns.Values;
                }
            }

            public virtual void Dispose()
            {
                if (Font != null)
                {
                    Font.Dispose();
                    Font = null;
                }
            }
        }

        public abstract class ListViewColumn
        {
            public event EventHandler<ListViewColumnMouseEventArgs> Click;

            protected ListViewColumn(int columnIndex)
            {
                if (columnIndex < 0)
                    throw new ArgumentException(null, "columnIndex");

                ColumnIndex = columnIndex;
            }

            public virtual ListViewExtender Extender { get; protected internal set; }
            public int ColumnIndex { get; private set; }

            public virtual Font Font
            {
                get
                {
                    return Extender == null ? null : Extender.Font;
                }
            }

            public ListView ListView
            {
                get
                {
                    return Extender == null ? null : Extender.ListView;
                }
            }

            public abstract void Draw(DrawListViewSubItemEventArgs e);

            public virtual void MouseClick(MouseEventArgs e, ListViewItem item, ListViewItem.ListViewSubItem subItem)
            {
                if (Click != null)
                {
                    Click(this, new ListViewColumnMouseEventArgs(e, item, subItem));
                }
            }

            public virtual void Invalidate(ListViewItem item, ListViewItem.ListViewSubItem subItem)
            {
                if (Extender != null)
                {
                    Extender.ListView.Invalidate(subItem.Bounds);
                }
            }
        }

        public class ListViewColumnMouseEventArgs : MouseEventArgs
        {
            public ListViewColumnMouseEventArgs(MouseEventArgs e, ListViewItem item, ListViewItem.ListViewSubItem subItem)
                : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
            {
                Item = item;
                SubItem = subItem;
            }

            public ListViewItem Item { get; private set; }
            public ListViewItem.ListViewSubItem SubItem { get; private set; }
        }


        public class ListViewButtonColumn : ListViewColumn
        {
            private Rectangle _hot = Rectangle.Empty;

            public ListViewButtonColumn(int columnIndex)
                : base(columnIndex)
            {
            }

            public bool FixedWidth { get; set; }
            public bool DrawIfEmpty { get; set; }

            public override ListViewExtender Extender
            {
                get
                {
                    return base.Extender;
                }
                protected internal set
                {
                    base.Extender = value;
                    if (FixedWidth)
                    {
                        base.Extender.ListView.ColumnWidthChanging += OnColumnWidthChanging;
                    }
                }
            }

            protected virtual void OnColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
            {
                if (e.ColumnIndex == ColumnIndex)
                {
                    e.Cancel = true;
                    e.NewWidth = ListView.Columns[e.ColumnIndex].Width;
                }
            }

            public override void Draw(DrawListViewSubItemEventArgs e)
            {
                if (_hot != Rectangle.Empty)
                {
                    if (_hot != e.Bounds)
                    {
                        ListView.Invalidate(_hot);
                        _hot = Rectangle.Empty;
                    }
                }

                if ((!DrawIfEmpty) && (string.IsNullOrEmpty(e.SubItem.Text)))
                    return;

                Point mouse = e.Item.ListView.PointToClient(Control.MousePosition);
                if ((ListView.GetItemAt(mouse.X, mouse.Y) == e.Item) && (e.Item.GetSubItemAt(mouse.X, mouse.Y) == e.SubItem))
                {
                    ButtonRenderer.DrawButton(e.Graphics, e.Bounds, e.SubItem.Text, Font, true, PushButtonState.Hot);
                    _hot = e.Bounds;
                }
                else
                {
                    ButtonRenderer.DrawButton(e.Graphics, e.Bounds, e.SubItem.Text, Font, false, PushButtonState.Default);
                }
            }
        }
        
        public Form1()
        {
            InitializeComponent();
            ListViewExtender extender = new ListViewExtender(listViewMatch);
            // extend 4rd column
            ListViewButtonColumn buttonAction = new ListViewButtonColumn(3);
            buttonAction.Click += OnButtonActionClick;
            buttonAction.FixedWidth = true;

            extender.AddColumn(buttonAction);
            /*
            for (int i = 0; i < 10000; i++)
            {
                ListViewItem item = listViewMatch.Items.Add("item" + i);
                item.SubItems.Add("button " + i);
            }
            */
        }
        public string[] RecupJoueurs(string equipe)
        {
            string[] TabJoueurs = new string[5];
            string url = "https://api.pandascore.co/csgo/teams?filter[name]=" + equipe + "&token=Z6NMfMOR_sgphX2aFt2PkJD1elVs6k7BGULPuj3WXpz2v__zd-4";
            WebRequest request = HttpWebRequest.Create(url);
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string teams_JSON = reader.ReadToEnd();

            teams myTeam = Newtonsoft.Json.JsonConvert.DeserializeObject<teams>(teams_JSON);

            myTeam.players.ToArray();
            for(int i = 0; i < 5; i++)
            {
                TabJoueurs[i] = myTeam.players[i].name;
            }
            return TabJoueurs;
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
            WebRequest request = HttpWebRequest.Create("https://api.pandascore.co/csgo/tournaments/running?token=Z6NMfMOR_sgphX2aFt2PkJD1elVs6k7BGULPuj3WXpz2v__zd-4");
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string RunningTournament_JSON = reader.ReadToEnd();

            RunningTournament[] myRunningTournanement = Newtonsoft.Json.JsonConvert.DeserializeObject<RunningTournament[]>(RunningTournament_JSON);

            listViewListeTournois.Items.Clear();

            foreach (RunningTournament tournament in myRunningTournanement)
            {
                string[] ligneSerieTn;
                if (tournament.end_at == null)
                {
                    ligneSerieTn = new string[] { "placeholder", tournament.serie.full_name, tournament.league.name, (tournament.begin_at.ToLocalTime()).ToString(), "Inconnue ou durée d'un jour", tournament.prizepool };
                }
                else
                {
                    ligneSerieTn = new string[] { "placeholder", tournament.serie.full_name, tournament.league.name, (tournament.begin_at.ToLocalTime()).ToString(), tournament.end_at.ToString(), tournament.prizepool };
                }
                var lvi = new ListViewItem(ligneSerieTn);
                lvi.Tag = ligneSerieTn;
                listViewListeTournois.Items.Add(lvi);
            }
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
                string[] ligneSerieTn;
                if (tournament.end_at == null)
                {
                    ligneSerieTn = new string[] { "placeholder", tournament.serie.full_name, tournament.league.name, (tournament.begin_at.ToLocalTime()).ToString(), "Inconnue ou durée d'un jour", tournament.prizepool };
                }
                else
                {
                    ligneSerieTn = new string[] { "placeholder", tournament.serie.full_name, tournament.league.name, (tournament.begin_at.ToLocalTime()).ToString(), tournament.end_at.ToString(), tournament.prizepool };
                }
                var lvi = new ListViewItem(ligneSerieTn);
                lvi.Tag = ligneSerieTn;
                listViewListeTournois.Items.Add(lvi);
            }
           
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panelAffichageTournois_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void buttonListMatchs_Click(object sender, EventArgs e)
        {
            WebRequest request = HttpWebRequest.Create("https://api.pandascore.co/csgo/matches/upcoming?sort=begin_at&token=Z6NMfMOR_sgphX2aFt2PkJD1elVs6k7BGULPuj3WXpz2v__zd-4");
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string UpcomingMatch_JSON = reader.ReadToEnd();

            UpcomingMatch[] myUpcomingMatch = Newtonsoft.Json.JsonConvert.DeserializeObject<UpcomingMatch[]>(UpcomingMatch_JSON);

            listViewMatch.Items.Clear();

            foreach (UpcomingMatch match in myUpcomingMatch)
            {
                string[] ligneSerieTn = new string[6];
                match.opponents.ToArray();
                if (match.opponents.Count() == 0)
                {
                    ligneSerieTn[0] = "TBD";
                    ligneSerieTn[1] = "TBD";
                }
                else if (match.opponents.Count() == 1) 
                {
                    ligneSerieTn[0] = match.opponents[0].opponent.name;
                    ligneSerieTn[1] = "TBD";
                }
                else
                {
                    ligneSerieTn[0] = match.opponents[0].opponent.name;
                    ligneSerieTn[1] = match.opponents[1].opponent.name;
                }
                ligneSerieTn[2] = match.tournament.slug;
                if (!match.live.supported)
                {
                    ligneSerieTn[3] = "Pas de live actuellement";
                }
                else
                {
                    ligneSerieTn[3] = match.live.opens_at.ToString();
                }
                ligneSerieTn[4] = (match.begin_at.ToLocalTime()).ToString();
                ligneSerieTn[5] = "BO" + match.number_of_games;

                //               if (match.live_url == "")
                //               {
                //               ligneSerieTn = new string[] { match.name, match.tournament.slug, match.live_url, (match.begin_at.ToLocalTime()).ToString(), "BO" + match.number_of_games };

                //               }
                //               else
                //               {
                //                   ligneSerieTn = new string[] { match.name, match.tournament.slug,"placeholder",/* match.live_url,*/ (match.begin_at.ToLocalTime()).ToString(), "BO" + match.number_of_games };
                //
                //               }

                var lvi = new ListViewItem(ligneSerieTn);
                lvi.Tag = ligneSerieTn;
                listViewMatch.Items.Add(lvi);
            }
        }

        private void buttonListLiveMatchs_Click(object sender, EventArgs e)
        {
            WebRequest request = HttpWebRequest.Create("https://api.pandascore.co/csgo/matches/running?sort=begin_at&token=Z6NMfMOR_sgphX2aFt2PkJD1elVs6k7BGULPuj3WXpz2v__zd-4");
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string MatchRunning_JSON = reader.ReadToEnd();

            MatchRunning[] myMatchRunning = Newtonsoft.Json.JsonConvert.DeserializeObject<MatchRunning[]>(MatchRunning_JSON);

            listViewMatch.Items.Clear();

            foreach (MatchRunning match in myMatchRunning)
            {
                string[] ligneSerieTn = new string[6];

                ligneSerieTn[0] = match.opponents[0].opponent.name;
                ligneSerieTn[1] = match.opponents[1].opponent.name;
                ligneSerieTn[2] = match.tournament.slug;
                ligneSerieTn[3] = match.live_url;
                ligneSerieTn[4] = (match.begin_at.ToLocalTime()).ToString();
                ligneSerieTn[5] = "BO" + match.number_of_games;

                //               if (match.live_url == "")
                //               {
                //                  ligneSerieTn = new string { match.name, match.tournament.slug, match.live_url, (match.begin_at.ToLocalTime()).ToString(), "BO" + match.number_of_games };

                //               }
                //               else
                //               {
                //                   ligneSerieTn = new string[] { match.name, match.tournament.slug,"placeholder",/* match.live_url,*/ (match.begin_at.ToLocalTime()).ToString(), "BO" + match.number_of_games };
                //
                //               }

                var lvi = new ListViewItem(ligneSerieTn);
                lvi.Tag = ligneSerieTn;
                listViewMatch.Items.Add(lvi);
            }
        }
        private void OnButtonActionClick(object sender, ListViewColumnMouseEventArgs e)
        {
            //MessageBox.Show(this, @"you clicked " + e.SubItem.Text);
            switch (e.SubItem.Text.Substring(0,4))
            {
                case "Indi": 
                    MessageBox.Show("Le match n'a pas encore débuté ou il n'existe pas de live pour ce match");
                    break;
                case "http":
                    System.Diagnostics.Process.Start(e.SubItem.Text);
                    break;
                case "Pas ":
                    MessageBox.Show("Le live n'est pas prévu");
                    break;
                default:
                    MessageBox.Show("Live prévu le " + e.SubItem.Text);
                    break;
            }
            
            
            /*if (e.SubItem.Text==("Indisponible"))
            {
                MessageBox.Show("Le match n'a pas encore débuté ou il n'existe pas de live pour ce match");
            }
            else if (e.SubItem.Text.Substring(0,4)=="http")
            {
                System.Diagnostics.Process.Start(e.SubItem.Text);
                //    webBrowser1.Navigate(e.SubItem.Text);
                //    webBrowser1.Url = new Uri(e.SubItem.Text);
            }
            else
            {
                MessageBox.Show("Live prévu le " + e.SubItem.Text);
            }*/
        }

        private void listViewMatch_MouseDoubleClick(object sender, MouseEventArgs e)
        {

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

public class Winner
{
    public int? id { get; set; }
    public string type { get; set; }
}

public class Game
{
    public DateTime? begin_at { get; set; }
    public bool detailed_stats { get; set; }
    public DateTime? end_at { get; set; }
    public bool finished { get; set; }
    public bool forfeit { get; set; }
    public int id { get; set; }
    public int? length { get; set; }
    public int match_id { get; set; }
    public int position { get; set; }
    public string status { get; set; }
    public object video_url { get; set; }
    public Winner winner { get; set; }
    public string winner_type { get; set; }
}

public class Opponent2
{
    public object acronym { get; set; }
    public int id { get; set; }
    public string image_url { get; set; }
    public string location { get; set; }
    public DateTime modified_at { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
}

public class Opponent
{
    public Opponent2 opponent { get; set; }
    public string type { get; set; }
}

public class Result
{
    public int score { get; set; }
    public int team_id { get; set; }
}

public class Tournament
{
    public DateTime begin_at { get; set; }
    public object end_at { get; set; }
    public int id { get; set; }
    public int league_id { get; set; }
    public bool live_supported { get; set; }
    public DateTime modified_at { get; set; }
    public string name { get; set; }
    public object prizepool { get; set; }
    public int serie_id { get; set; }
    public string slug { get; set; }
    public object winner_id { get; set; }
    public object winner_type { get; set; }
}

public class MatchRunning
{
    public DateTime begin_at { get; set; }
    public bool detailed_stats { get; set; }
    public bool draw { get; set; }
    public object end_at { get; set; }
    public bool forfeit { get; set; }
    public object game_advantage { get; set; }
    public List<Game> games { get; set; }
    public int id { get; set; }
    public League league { get; set; }
    public int league_id { get; set; }
    public Live live { get; set; }
    public string live_url { get; set; }
    public string match_type { get; set; }
    public DateTime modified_at { get; set; }
    public string name { get; set; }
    public int number_of_games { get; set; }
    public List<Opponent> opponents { get; set; }
    public DateTime original_scheduled_at { get; set; }
    public bool rescheduled { get; set; }
    public List<Result> results { get; set; }
    public DateTime scheduled_at { get; set; }
    public Serie serie { get; set; }
    public int serie_id { get; set; }
    public string slug { get; set; }
    public string status { get; set; }
    public Tournament tournament { get; set; }
    public int tournament_id { get; set; }
    public Videogame videogame { get; set; }
    public object videogame_version { get; set; }
    public object winner { get; set; }
    public object winner_id { get; set; }
}

    public class UpcomingMatch
    {
        public DateTime begin_at { get; set; }
        public bool detailed_stats { get; set; }
        public bool draw { get; set; }
        public object end_at { get; set; }
        public bool forfeit { get; set; }
        public int? game_advantage { get; set; }
        public List<Game> games { get; set; }
        public int id { get; set; }
        public League league { get; set; }
        public int league_id { get; set; }
        public Live live { get; set; }
        public string live_url { get; set; }
        public string match_type { get; set; }
        public DateTime modified_at { get; set; }
        public string name { get; set; }
        public int number_of_games { get; set; }
        public List<Opponent> opponents { get; set; }
        public DateTime original_scheduled_at { get; set; }
        public bool rescheduled { get; set; }
        public List<object> results { get; set; }
        public DateTime scheduled_at { get; set; }
        public Serie serie { get; set; }
        public int serie_id { get; set; }
        public string slug { get; set; }
        public string status { get; set; }
        public Tournament tournament { get; set; }
        public int tournament_id { get; set; }
        public Videogame videogame { get; set; }
        public object videogame_version { get; set; }
        public object winner { get; set; }
        public object winner_id { get; set; }
    }
}

public class Player
{
    public int birth_year { get; set; }
    public string birthday { get; set; }
    public string first_name { get; set; }
    public string hometown { get; set; }
    public int id { get; set; }
    public string image_url { get; set; }
    public string last_name { get; set; }
    public string name { get; set; }
    public string nationality { get; set; }
    public object role { get; set; }
    public string slug { get; set; }
}

public class teams
{
    public object acronym { get; set; }
    public int id { get; set; }
    public string image_url { get; set; }
    public string location { get; set; }
    public DateTime modified_at { get; set; }
    public string name { get; set; }
    public List<Player> players { get; set; }
    public string slug { get; set; }
}