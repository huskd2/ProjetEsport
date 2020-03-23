namespace ProjetEsport
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMenu = new System.Windows.Forms.Panel();
            this.buttonMatchs = new System.Windows.Forms.Button();
            this.buttonTournois = new System.Windows.Forms.Button();
            this.buttonListLiveMatchs = new System.Windows.Forms.Button();
            this.buttonListMatchs = new System.Windows.Forms.Button();
            this.panelMenuTournois = new System.Windows.Forms.Panel();
            this.buttonTournoisRunning = new System.Windows.Forms.Button();
            this.buttonTournoisUpcoming = new System.Windows.Forms.Button();
            this.panelAffichageTournois = new System.Windows.Forms.Panel();
            this.listViewListeTournois = new System.Windows.Forms.ListView();
            this.thumbnailID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDateDebut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDateFin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnNameTn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnNameSeries = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPrizepool = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelMenu.SuspendLayout();
            this.panelMenuTournois.SuspendLayout();
            this.panelAffichageTournois.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMenu.Controls.Add(this.buttonMatchs);
            this.panelMenu.Controls.Add(this.buttonTournois);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(118, 485);
            this.panelMenu.TabIndex = 0;
            // 
            // buttonMatchs
            // 
            this.buttonMatchs.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonMatchs.Location = new System.Drawing.Point(0, 0);
            this.buttonMatchs.Name = "buttonMatchs";
            this.buttonMatchs.Size = new System.Drawing.Size(116, 219);
            this.buttonMatchs.TabIndex = 1;
            this.buttonMatchs.Text = "Matchs";
            this.buttonMatchs.UseVisualStyleBackColor = true;
            this.buttonMatchs.Click += new System.EventHandler(this.buttonMatchs_Click);
            // 
            // buttonTournois
            // 
            this.buttonTournois.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonTournois.Location = new System.Drawing.Point(0, 218);
            this.buttonTournois.Name = "buttonTournois";
            this.buttonTournois.Size = new System.Drawing.Size(116, 265);
            this.buttonTournois.TabIndex = 0;
            this.buttonTournois.Text = "Tournois";
            this.buttonTournois.UseVisualStyleBackColor = true;
            this.buttonTournois.Click += new System.EventHandler(this.buttonTournois_Click);
            // 
            // buttonListLiveMatchs
            // 
            this.buttonListLiveMatchs.Location = new System.Drawing.Point(281, 12);
            this.buttonListLiveMatchs.Name = "buttonListLiveMatchs";
            this.buttonListLiveMatchs.Size = new System.Drawing.Size(105, 40);
            this.buttonListLiveMatchs.TabIndex = 2;
            this.buttonListLiveMatchs.Text = "Liste des matchs en live";
            this.buttonListLiveMatchs.UseVisualStyleBackColor = true;
            // 
            // buttonListMatchs
            // 
            this.buttonListMatchs.Location = new System.Drawing.Point(537, 12);
            this.buttonListMatchs.Name = "buttonListMatchs";
            this.buttonListMatchs.Size = new System.Drawing.Size(102, 39);
            this.buttonListMatchs.TabIndex = 3;
            this.buttonListMatchs.Text = "Liste de tout les matchs";
            this.buttonListMatchs.UseVisualStyleBackColor = true;
            // 
            // panelMenuTournois
            // 
            this.panelMenuTournois.Controls.Add(this.buttonTournoisRunning);
            this.panelMenuTournois.Controls.Add(this.buttonTournoisUpcoming);
            this.panelMenuTournois.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenuTournois.Location = new System.Drawing.Point(118, 0);
            this.panelMenuTournois.Name = "panelMenuTournois";
            this.panelMenuTournois.Size = new System.Drawing.Size(874, 74);
            this.panelMenuTournois.TabIndex = 4;
            this.panelMenuTournois.Visible = false;
            // 
            // buttonTournoisRunning
            // 
            this.buttonTournoisRunning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTournoisRunning.Location = new System.Drawing.Point(0, 0);
            this.buttonTournoisRunning.Name = "buttonTournoisRunning";
            this.buttonTournoisRunning.Size = new System.Drawing.Size(874, 36);
            this.buttonTournoisRunning.TabIndex = 1;
            this.buttonTournoisRunning.Text = "Tournois en cours";
            this.buttonTournoisRunning.UseVisualStyleBackColor = true;
            this.buttonTournoisRunning.Click += new System.EventHandler(this.buttonTournoisRunning_Click);
            // 
            // buttonTournoisUpcoming
            // 
            this.buttonTournoisUpcoming.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonTournoisUpcoming.Location = new System.Drawing.Point(0, 36);
            this.buttonTournoisUpcoming.Name = "buttonTournoisUpcoming";
            this.buttonTournoisUpcoming.Size = new System.Drawing.Size(874, 38);
            this.buttonTournoisUpcoming.TabIndex = 0;
            this.buttonTournoisUpcoming.Text = "Prochains tournois";
            this.buttonTournoisUpcoming.UseVisualStyleBackColor = true;
            this.buttonTournoisUpcoming.Click += new System.EventHandler(this.buttonTournoisUpcoming_Click);
            // 
            // panelAffichageTournois
            // 
            this.panelAffichageTournois.Controls.Add(this.listViewListeTournois);
            this.panelAffichageTournois.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAffichageTournois.Location = new System.Drawing.Point(118, 74);
            this.panelAffichageTournois.Name = "panelAffichageTournois";
            this.panelAffichageTournois.Size = new System.Drawing.Size(874, 411);
            this.panelAffichageTournois.TabIndex = 5;
            this.panelAffichageTournois.Visible = false;
            // 
            // listViewListeTournois
            // 
            this.listViewListeTournois.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.thumbnailID,
            this.columnNameSeries,
            this.columnNameTn,
            this.columnDateDebut,
            this.columnDateFin,
            this.columnPrizepool});
            this.listViewListeTournois.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewListeTournois.FullRowSelect = true;
            this.listViewListeTournois.GridLines = true;
            this.listViewListeTournois.HideSelection = false;
            this.listViewListeTournois.Location = new System.Drawing.Point(0, 0);
            this.listViewListeTournois.Name = "listViewListeTournois";
            this.listViewListeTournois.Size = new System.Drawing.Size(874, 411);
            this.listViewListeTournois.TabIndex = 0;
            this.listViewListeTournois.UseCompatibleStateImageBehavior = false;
            this.listViewListeTournois.View = System.Windows.Forms.View.Details;
            this.listViewListeTournois.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // thumbnailID
            // 
            this.thumbnailID.Text = "Miniature";
            this.thumbnailID.Width = 100;
            // 
            // columnDateDebut
            // 
            this.columnDateDebut.Text = "Date de début";
            this.columnDateDebut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnDateDebut.Width = 100;
            // 
            // columnDateFin
            // 
            this.columnDateFin.Text = "Date de fin";
            this.columnDateFin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnDateFin.Width = 100;
            // 
            // columnNameTn
            // 
            this.columnNameTn.Text = "Tournois";
            this.columnNameTn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnNameTn.Width = 100;
            // 
            // columnNameSeries
            // 
            this.columnNameSeries.Text = "Series";
            this.columnNameSeries.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnNameSeries.Width = 100;
            // 
            // columnPrizepool
            // 
            this.columnPrizepool.Text = "Prizepool";
            this.columnPrizepool.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnPrizepool.Width = 100;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 485);
            this.Controls.Add(this.panelAffichageTournois);
            this.Controls.Add(this.panelMenuTournois);
            this.Controls.Add(this.buttonListMatchs);
            this.Controls.Add(this.buttonListLiveMatchs);
            this.Controls.Add(this.panelMenu);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelMenuTournois.ResumeLayout(false);
            this.panelAffichageTournois.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button buttonMatchs;
        private System.Windows.Forms.Button buttonTournois;
        private System.Windows.Forms.Button buttonListLiveMatchs;
        private System.Windows.Forms.Button buttonListMatchs;
        private System.Windows.Forms.Panel panelMenuTournois;
        private System.Windows.Forms.Button buttonTournoisRunning;
        private System.Windows.Forms.Button buttonTournoisUpcoming;
        private System.Windows.Forms.Panel panelAffichageTournois;
        private System.Windows.Forms.ListView listViewListeTournois;
        private System.Windows.Forms.ColumnHeader thumbnailID;
        private System.Windows.Forms.ColumnHeader columnNameSeries;
        private System.Windows.Forms.ColumnHeader columnNameTn;
        private System.Windows.Forms.ColumnHeader columnDateDebut;
        private System.Windows.Forms.ColumnHeader columnDateFin;
        private System.Windows.Forms.ColumnHeader columnPrizepool;
    }
}

