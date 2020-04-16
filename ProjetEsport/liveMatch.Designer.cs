namespace ProjetEsport
{
    partial class liveMatch
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonTournoisRunning = new System.Windows.Forms.Button();
            this.buttonTournoisUpcoming = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonTournoisRunning
            // 
            this.buttonTournoisRunning.Location = new System.Drawing.Point(0, 0);
            this.buttonTournoisRunning.Name = "buttonTournoisRunning";
            this.buttonTournoisRunning.Size = new System.Drawing.Size(796, 37);
            this.buttonTournoisRunning.TabIndex = 1;
            this.buttonTournoisRunning.Text = "Tournois en cours";
            this.buttonTournoisRunning.UseVisualStyleBackColor = true;
            // 
            // buttonTournoisUpcoming
            // 
            this.buttonTournoisUpcoming.Location = new System.Drawing.Point(0, 33);
            this.buttonTournoisUpcoming.Name = "buttonTournoisUpcoming";
            this.buttonTournoisUpcoming.Size = new System.Drawing.Size(796, 40);
            this.buttonTournoisUpcoming.TabIndex = 0;
            this.buttonTournoisUpcoming.Text = "Prochains tournois";
            this.buttonTournoisUpcoming.UseVisualStyleBackColor = true;
            // 
            // liveMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonTournoisUpcoming);
            this.Controls.Add(this.buttonTournoisRunning);
            this.Name = "liveMatch";
            this.Size = new System.Drawing.Size(796, 73);
            this.Load += new System.EventHandler(this.liveMatch_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonTournoisRunning;
        private System.Windows.Forms.Button buttonTournoisUpcoming;
    }
}
