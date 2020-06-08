
using CustomEventArgs;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq.Expressions;

namespace Proyectog15WF
{

    public partial class AppForm : Form
    {
        bool pasua = false;
        int resultCounter = 0;
        string namevideo = "";
        string namesong = "";
        string nameuser = ""; //ESTE ES EL NO,BRE DEL USUARIO
        string ageAcctounte = ""; //EDAD DEL USUARIO
        string GenderAccounte = "";//GENERO DEL USUARIO;
        bool buttonClickdelete = false;
        bool queuecheck = true;
        bool emptyqueue = false;
        Song variablecancion = null; //CANCION SELECCIONADA
        Video variablevideo = null;
        List<Song> cancionesdelusuario = new List<Song>(); //LISTA DE CANCIONES DEL USUARIO
        List<Video> videosdelusuario = new List<Video>();
        List<Song> songqueuelist = new List<Song>();
        List<Video> videoqueuelist = new List<Video>();
        List<Song> historialsong = new List<Song>();
        List<Video> historialvideo = new List<Video>();
        Song songbeingreproduced = null;
        public delegate bool LoginEventHandler(object source, LoginEventArgs args);
        public event LoginEventHandler LoginButtonClicked;
        public event EventHandler<LoginEventArgs> UserChecked;
        public delegate bool RegisterEventHandler(object source, RegisterEventArgs args);
        public event RegisterEventHandler RegisterButtonClicked;
        public delegate string CheckusernameEventHandler(object source, RegisterEventArgs args);
        public event CheckusernameEventHandler Checkusernameregister;
        public event EventHandler<SearchUserEventArgs> Searching;
        public event EventHandler<SearchingSongorVideo> Searchingnamevideoorsong;
        public delegate User LoginReturnUserEventHandler(object source, LoginEventArgs args);
        public event LoginReturnUserEventHandler Userrequest;
        public delegate Artist GetingArtistEventHandler(object source, GetArtistEventArgs args);
        public event GetingArtistEventHandler getArtist;
        public event EventHandler<ChangeImageEventsArgs> changeImage;
        // Eventos playlist
        public delegate List<PlaylistSong> SendingPlaylistHandler(object source, GetUserPlaylistEventsArgs args);
        public event SendingPlaylistHandler Sendingplaylist;
        public event SendingPlaylistHandler Sendingfollowedplaylist;
        public delegate List<PlaylistVideo> SendingPlaylistVideoHandler(object source, GetUserPlaylistEventsArgs args);
        public event SendingPlaylistVideoHandler SendingplaylistVideo;
        public event SendingPlaylistVideoHandler Sendingfollowedplaylistvideo;
        public event EventHandler<GetUserPlaylistEventsArgs> Addvideoplaylist;
        public delegate bool SendingActualPlaylistHandler(object source, GetUserPlaylistEventsArgs args);
        public event SendingActualPlaylistHandler Userselectedplaylist;
        public event SendingActualPlaylistHandler Userselectedvideoplaylist;
        public event SendingActualPlaylistHandler Userselectedfollowedplaylist;
        public event SendingActualPlaylistHandler Userselectedfollowedvideoplaylist;

        public delegate List<Song> SendingSongQueueHandler(object source, GetUserPlaylistEventsArgs args);
        public event SendingSongQueueHandler CreateSongQueue;
        public delegate List<Video> SendingVideoQueueHandler(object source, GetUserPlaylistEventsArgs args);
        public event SendingVideoQueueHandler CreateVideoQueue;

        public event EventHandler<GetUserPlaylistEventsArgs> Addplaylist;
        public event EventHandler<GetUserPlaylistEventsArgs> Followmusicplaylist;
        public event EventHandler<GetUserPlaylistEventsArgs> Followvideoplaylist;

        // Eventos seguir usuarios
        public delegate List<User> SendingFollowUserHandler(object source, GetUserPlaylistEventsArgs args);
        public event EventHandler<GetUserPlaylistEventsArgs> AddFollowedUser;
        public event EventHandler<GetUserPlaylistEventsArgs> AddFollowingUser;
        public event EventHandler<GetUserPlaylistEventsArgs> RemoveFollowedUser;
        public event EventHandler<GetUserPlaylistEventsArgs> RemoveFollowingUser;
        public event SendingFollowUserHandler ShowFollowedUsers;
        public event SendingFollowUserHandler ShowFollowingUsers;

        //Eventos privacidad
        public event EventHandler<GetUserPlaylistEventsArgs> SetPlaylistSongPrivacy;
        public event EventHandler<GetUserPlaylistEventsArgs> SetPlaylistVideoPrivacy;

        //Evnetos de reproduccion
        public delegate string SelectedVideoEventHandler(object source, SelectVideoEventArgs args);
        public event SelectedVideoEventHandler Reproducevideo;
        public delegate string SelectedSongEventHandler(object source, SelectSongEventArgs args);
        public event SelectedSongEventHandler Reproducesong;

        //Evento cambiar contraseña
        public delegate bool ChangepasswordEvnetHandler(object source, ChangePasswordEventArgs args);
        public event ChangepasswordEvnetHandler Changingpassword;
        //Evento para agregar cacnion a la playlist
        public delegate Song ReceiveSongEventHandler(object source, ReturnsongEventArgs args);
        public event ReceiveSongEventHandler Recivingsong;
        //Evento agregar video
        public delegate Video ReciveVideoEventHandler(object source, ReturnVideoEventArgs args);
        public event ReciveVideoEventHandler Recivingvideo;
        //Evento para enviar cambios del usuario
        public event EventHandler<SendingtypeaccountEventArgs> Userifosend;
        //Evento para envar Artista
        public event EventHandler<SendingArtistInfo> Artistifosend;
        //Evento´para serializar
        public event EventHandler<EventArgs> Serialize;
        //Evento para llamar al artista
        public delegate string TypeartistEventHandler(object source, LoginEventArgs args);
        public event TypeartistEventHandler Artistwithcaracteristics;
        //Evento para mandar la cancion
        public event EventHandler<SendingsongcaracteristicsEventArgs> Songcaracteristics;
        //Evento para mandar el video
        public event EventHandler<SendingvideocaracteristicsEventArgs> Videocaracteristics;
        //Evento para recibir la lista de canciones
        public delegate List<string> SongsEventHandler(object source, SendingSongsEventArgs args);
        public event SongsEventHandler Totalitsofsongs;
        //Evento para recibir la lista de videos
        public delegate List<string> VideosEventHandler(object source, SendingVideosEventArgs args);
        public event VideosEventHandler Totalitsofvideos;
        // ver si existe la cancion
        public delegate bool VerfysongEventHandler(object source, SongsExistEventsArtgs args);
        public event VerfysongEventHandler verfyedsong;
        //Ver si el video existe
        public delegate bool VerfyvideoEventHandler(object source, VideosExistEventsArtgs args);
        public event VerfyvideoEventHandler verifyVideoExist;
        //Evento para busqueda multiple
        public delegate List<Song> SendingMultipleSong(object source, SendingtextMultipleFiltersEventArgs args);
        public event SendingMultipleSong Recivesongmultiplecriteria;
        //Evento multiples filtros videos
        public delegate List<Video> SendingMultiplevideos(object source, SendingtextMultipleFiltersEventArgs args);
        public event SendingMultiplevideos Recivingvideomultiplecriteria;
        //Evento que manda al Artista
        public event EventHandler<ArtistInfoEventArgs> Artistinfo;
        //Evento para subir reproducciones
        public event EventHandler<ReproduccionesEventArgs> Reproduccionesname;
        //Evento para mandar la calificacion
        public event EventHandler<MandarcalficacionEventArgs> Calificaciondelusuario;
        //Evento para verificar mail
        public delegate string CheckMailEventArgs(object source, MailEventArgs args);
        public event CheckMailEventArgs MailVerifyEvent;




        List<Panel> stackPanels = new List<Panel>();
        Dictionary<string, Panel> panels = new Dictionary<string, Panel>();
        User actuallogeduser = null;
        bool prim = false;


        public AppForm()
        {

            InitializeComponent();
            panels.Add("StartPanel", StartPanel);
            panels.Add("Registerpanel", RegisterPanel);
            panels.Add("LoginPanel", LoginPanel);
            panels.Add("Mainpanel", MainPanel);
        }

        public void SerializeData()
        {
            if (Serialize != null)
            {
                Serialize(this, new EventArgs() { });
            }
        }
        private void HideSubPanel()

        {
            SerializeData();
            if (SubArtistPanel.Visible == true)
            {
                SubArtistPanel.Visible = false;
            }
            if (SubPlaylistPanel.Visible == true)
            {
                SubPlaylistPanel.Visible = false;
            }
            if (SubProfilePanel.Visible == true)
            {
                SubProfilePanel.Visible = false;
            }
            if (SubSerchPanel.Visible == true)
            {
                SubSerchPanel.Visible = false;
            }
        }
        private void ShowSubPanel(Panel submenu)
        {
            SerializeData();

            if (submenu.Visible == false)
            {
                HideSubPanel();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }
        }

        private void EXITbutton_Click(object sender, EventArgs e)
        {
            SerializeData();


            this.Close();
        }


        private void RegistrateButton_Click(object sender, EventArgs e)
        {


            StartPanel.SendToBack();
            RegisterPanel.BringToFront();
            RegisterPanel.Visible = true;
            SerializeData();

        }

        private void BackButtonLogin_Click(object sender, EventArgs e)
        {

            SerializeData();
            LoginPanel.SendToBack();
            StartPanel.BringToFront();
            LoginPanel.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SerializeData();
            StartPanel.SendToBack();
            LoginPanel.BringToFront();
            loginViewInvalidCredentialsAlert.ResetText();
            loginViewInvalidCredentialsAlert.Visible = false;
            //SaveLogin.ResumeLayout();
            LoginPanel.Visible = true;

        }

        private void BackRegisterButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            RegisterPanel.SendToBack();
            StartPanel.BringToFront();
            RegisterPanel.Visible = false;

        }


        private void OnLoginButtonClicked(string username, string pass)
        {
            SerializeData();
            if (LoginButtonClicked != null)
            {
                bool result = LoginButtonClicked(this, new LoginEventArgs() { UsernameText = username, PasswordText = pass });
                if (!result)
                {
                    UsernameInPutLogin.ResetText();
                    PasswordInPutLogin.ResetText();
                    loginViewInvalidCredentialsAlert.Text = "Credenciales invalidas";
                    loginViewInvalidCredentialsAlert.Visible = true;
                }
                else
                {
                    if (Userrequest != null)
                    {
                        actuallogeduser = Userrequest(this, new LoginEventArgs() { UsernameText = username });

                    }
                    {
                        string variable = actuallogeduser.Genero;
                        if (variable == "Hombre")
                        {
                            GeneroComboBox.SelectedIndex = 0;
                        }
                        if (variable == "Mujer")
                        {

                            GeneroComboBox.SelectedIndex = 1;
                        }
                        if (variable == "Otro")
                        {

                            GeneroComboBox.SelectedIndex = 2;
                        }
                        if (variable == "None")
                        {

                            GeneroComboBox.SelectedIndex = 3;
                        }
                    }

                    if (actuallogeduser.Artist != null)
                    {
                        VeryfyArtistPanel.Visible = false;
                        //prim = true;
                    }
                    else
                    {
                        VeryfyArtistPanel.Visible = true;
                        //prim = false;
                    }

                    if (actuallogeduser.Tipodeusuario.ToUpper() == "PREMIUM")
                    {
                        TipoDeCuentaCombobox.SelectedIndex = 1;
                        prim = true;
                    }
                    else
                    {
                        prim = false;
                        TipoDeCuentaCombobox.SelectedIndex = 0;
                        NotPrimiumLabelArtist.Visible = true;
                        TipoArtistacomboBox1.Visible = false;
                        TipodeArtistaModeLabel.Visible = false;
                        TipoArtistaButton.Visible = false;

                    }
                    if (actuallogeduser.Privacidad == "Privado")
                    {
                        PrivacidadInputCuenta.SelectedIndex = 1;
                    }
                    if (actuallogeduser.Privacidad == "Publico")
                    {
                        PrivacidadInputCuenta.SelectedIndex = 0;
                    }

                    NombreCuentaImput.Text = actuallogeduser.Name;
                    ApellidoCuentaInput.Text = actuallogeduser.Lastname;
                    UsuarioCuentaInput.Text = actuallogeduser.Username;
                    MailCuentaInput.Text = actuallogeduser.Mail;

                    if (actuallogeduser.Edad != "")
                    {
                        EdadCuentaInput.Text = actuallogeduser.Edad;
                        EdadCuentaInput.ReadOnly = true;
                    }
                    else
                    {
                        EdadCuentaInput.Text = null;
                        EdadCuentaInput.ReadOnly = false;
                    }
                    if (actuallogeduser.ImagePast != "")
                    {
                        Bitmap image = new Bitmap(actuallogeduser.ImagePast);
                        pictureBox1.BackgroundImage = null;
                        pictureBox2.BackgroundImage = null; 
                        pictureBox1.Image = image;
                        pictureBox2.Image = image;
                    }
                    

                    loginViewInvalidCredentialsAlert.ResetText();
                    loginViewInvalidCredentialsAlert.Visible = false;
                    OnUserChecked(username, pass);
                    actuallogeduser = Userrequest(this, new LoginEventArgs() { UsernameText = nameuser });
                    foreach (PlaylistSong playlist in actuallogeduser.GetPlaylistSongs())
                    {
                        playlist.ClearQueue();
                    }
                    foreach (PlaylistVideo playlist in actuallogeduser.GetPlaylistVideo())
                    {
                        playlist.ClearQueue();
                    }
                }
            }
        }
        private void OnUserChecked(string username, string password)
        {
            if (UserChecked != null)
            {
                UserChecked(this, new LoginEventArgs() { UsernameText = username, PasswordText = password });
                UsernameInPutLogin.ResetText();
                PasswordInPutLogin.ResetText();
                nameuser = username; //AQUI OBTENGO EL USUARIO QUE HACE LOGIN
                if (Userrequest != null)
                {
                    actuallogeduser = Userrequest(this, new LoginEventArgs() { UsernameText = username });
                }
                stackPanels.Add(panels["StartPanel"]);
                MainPanel.Visible = true;
                MainPanel.BringToFront();

                //ShowLastPanel();
            }
        }

        public void Onregisteredbutooncliked(string Name, string Lastname, string Username, string Mailuser, string Passworduser)
        {
            SerializeData();
            if (RegisterButtonClicked != null)
            {
                bool result = RegisterButtonClicked(this, new RegisterEventArgs() { Usernametext = Username, Passwordtext = Passworduser, Nametext = Name, Lastnametext = Lastname, Email = Mailuser });
                if (result)
                {
                    nameInputRegister.ResetText();
                    LastNameInputRegister.ResetText();
                    UsernameInputRegister.ResetText();
                    MailInputRegister.ResetText();
                    PasswordInputRegister.ResetText();
                    stackPanels.Add(panels["StartPanel"]);
                    ShowLastPanel();
                }
            }

        }
        private void ShowLastPanel()
        {
            SerializeData();
            foreach (Panel panel in panels.Values)
            {
                if (panel != stackPanels.Last())
                {
                    panel.Visible = false;
                }
                else
                {
                    panel.Visible = true;
                }
            }
        }

        private void InicioLoginButton_Click(object sender, EventArgs e)
        {
            string username = UsernameInPutLogin.Text;
            string pass = PasswordInPutLogin.Text;
            SerializeData();
            if (username == "Admin" && pass == "Admin123")
            {
                AdminMainPanel.Visible = true;
            }
            else
            {
                OnLoginButtonClicked(username, pass);
            }

        }

        private void Registerbutton_Click(object sender, EventArgs e)
        {
            SerializeData();
            string nameInputuser = null;

            string lastNameInputuser = null;

            string usernameInputuser = null;
            string mailInputuser = null;

            string passwordInputUser = null;
            int count = 0;
            if (count == 0)
            {
                nameInputuser = nameInputRegister.Text;
                if (nameInputuser == "")
                {
                    MessageBox.Show("Nombre incorrecto");
                }
                else
                {
                    count++;
                }
                lastNameInputuser = LastNameInputRegister.Text;
                if (lastNameInputuser == "")
                {
                    MessageBox.Show("Apellido incorrecto");
                }
                else
                {
                    count++;
                }
                usernameInputuser = UsernameInputRegister.Text;
                if (usernameInputuser == "" || usernameInputuser.Length <= 3)
                {
                    MessageBox.Show("Su usuario debe tener minimo 4 caracteres");
                }
                else if (OncheckUsernameregister(usernameInputuser) != null)
                {
                    MessageBox.Show("Ya existe este nombre de usuario");
                }
                else
                {
                    count++;
                }

                mailInputuser = MailInputRegister.Text;
                if (mailInputuser == "")
                {
                    MessageBox.Show("Mail incorrecto");
                }
                else if (OncheckMail(mailInputuser) != null)
                {
                    MessageBox.Show("Ya existe este correo");
                }
                else
                {
                    count++;
                }

                passwordInputUser = PasswordInputRegister.Text;
                if (passwordInputUser == "" | passwordInputUser.Length < 8)
                {
                    MessageBox.Show("Su contraseña debe ser de largo 8 o mas");

                }
                else
                {
                    count++;
                }
                if (count == 5)
                {
                    Onregisteredbutooncliked(nameInputuser, lastNameInputuser, usernameInputuser, mailInputuser, passwordInputUser);
                    MessageBox.Show("Gracias por unirte a SpotFlix" + " " + usernameInputuser);
                    count = 0;
                }



            }



        }
        public string OncheckUsernameregister(string Username)
        {
            SerializeData();
            string check = null;
            if (Checkusernameregister != null)
            {
                if (Username == Checkusernameregister(this, new RegisterEventArgs() { Usernametext = Username }))
                {
                    check += Username;
                }
                else
                {
                    check = null;
                }
            }
            return check;

        }
        public string OncheckMail(string Mail)
        {
            SerializeData();
            string check = null;
            if (MailVerifyEvent != null)
            {
                if (Mail == MailVerifyEvent(this, new MailEventArgs() { Emailtext = Mail }))
                {
                    check += Mail;
                }
                else
                {
                    check = null;
                }
            }
            return check;


        }

        private void UserSeachButton_Click(object sender, EventArgs e)
        {
            SearchUserPanelResultlistusers.Items.Clear();
            SearchUserPaneltextbox.ResetText();
            SerializeData();
            //MainScreenPanel.Visible = false;
            SearchMediapanel.Visible = false;
            SearchArtistPanel.Visible = false;
            SearchMainPanel.Visible = true;
            SearchUserPanel.Visible = true;
            SeguirPlaylistPanel.Visible = false;

        }

        private void SearchButton_Click_1(object sender, EventArgs e)
        {
            SerializeData();
            ShowSubPanel(SubSerchPanel);
            ReproduccionMainPanel.Visible = false;
            PlaylistMainPanel.Visible = false;
            ArtistModeMainPanel.Visible = false;
            ProfileMainPanel.Visible = false;

            if (SearchMainPanel.Visible)
            {
                MainScreenPanel.Visible = true;
                SearchUserPanel.Visible = false;
                SearchMediapanel.Visible = false;
                SearchArtistPanel.Visible = false;
                SearchMainPanel.Visible = false;
            }
            else
            {
                //SearchMainPanel.Visible = true;
                //SearchMediapanel.Visible = true;
                // SearchUserPanel.Visible = false;
                //ArtistSearchPanel.Visible = false;
                //SearchMediapanel.Visible = false;
            }
        }

        private void PlayListButton_Click_1(object sender, EventArgs e)
        {
            SerializeData();
            ShowSubPanel(SubPlaylistPanel);
            MainScreenPanel.Visible = true;
            SearchMainPanel.Visible = false;
            ArtistModeMainPanel.Visible = false;
            ProfileMainPanel.Visible = false;
            ReproduccionMainPanel.Visible = false;
            queuecheck = true;
            if (PlaylistMainPanel.Visible)
            {
                PlaylistMainPanel.Visible = false;
            }
            else
            {
                PlaylistMainPanel.Visible = false;
            }

        }

        private void ArtisteModeButton_Click(object sender, EventArgs e)
        {
            ShowSubPanel(SubArtistPanel);
            SerializeData();

            if (!prim)
            {
                VeryfyArtistPanel.Visible = true;
            }

            ReproduccionMainPanel.Visible = false;
            MainScreenPanel.Visible = true;
            SearchMainPanel.Visible = false;
            PlaylistMainPanel.Visible = false;
            ArtistModeMainPanel.Visible = false;
            ProfileMainPanel.Visible = false;

        }

        private void ProfileButton_Click_1(object sender, EventArgs e)
        {
            SerializeData();
            ShowSubPanel(SubProfilePanel);
            MainScreenPanel.Visible = true;
            SearchMainPanel.Visible = false;
            PlaylistMainPanel.Visible = false;
            ArtistModeMainPanel.Visible = false;
            ProfileMainPanel.Visible = false;
            ReproduccionMainPanel.Visible = false;
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            //stackPanels.RemoveAt(stackPanels.Count - 1);
            // ShowLastPanel();
            //axWindowsMediaPlayer1.Ctlcontrols.pause();
            SerializeData();
            MainPanel.Visible = false;
            SubArtistPanel.Visible = false;
            SubPlaylistPanel.Visible = false;
            SubProfilePanel.Visible = false;
            SubSerchPanel.Visible = false;
            MainScreenPanel.Visible = true;
            SearchMainPanel.Visible = false;
            PlaylistMainPanel.Visible = false;
            ArtistModeMainPanel.Visible = false;
            ProfileMainPanel.Visible = false;
            ReproduccionMainPanel.Visible = false;
            AlbumCanciones.Items.Clear();
            VideoAlbumListBox.Items.Clear();
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            namevideo = "";
            namesong = "";
            pasua = false;
            pictureBox1.Image = null;
            pictureBox2.Image = null;


        }

        private void SearchUserPaneltextbox_TextChanged(object sender, EventArgs e)
        {
            SerializeData();
            string searchtext = SearchUserPaneltextbox.Text;
            List<string> results = new List<string>();
            if (searchtext.Length >= 3)
            {
                CleanSearch();
                Noresult();
                if (Searching != null)
                {
                    Searching(this, new SearchUserEventArgs() { SearchText = searchtext });
                }

            }
        }
        private void Noresult()
        {
            SerializeData();
            SearchUserPanelResultlistusers.Items.Add("No results for search criteria");
            AdminSearchUserlistBox.Items.Add("No results for search criteria");
            SearchArtistListBox.Items.Add("No results for search criteria");
            AdminSearchAristlistBox1.Items.Add("No results for search criteria");
        }
        private void CleanSearch()
        {
            resultCounter = 0;
            SearchUserPanelResultlistusers.Items.Clear();
            AdminSearchUserlistBox.Items.Clear();
            SearchArtistListBox.Items.Clear();
            AdminSearchAristlistBox1.Items.Clear();

        }
        public void UpdateResults(List<string> results)
        {
            if (results.Count > 0)
            {
                foreach (string result in results)
                {
                    if (resultCounter <= 50)
                    {
                        if (SearchUserPanelResultlistusers.Items.Count > 0 && SearchUserPanelResultlistusers.Items[0].Equals("No results for search criteria"))
                        {
                            SearchUserPanelResultlistusers.Items.Add(result);
                            SearchUserPanelResultlistusers.Items.RemoveAt(0);

                        }
                        else
                        {
                            SearchUserPanelResultlistusers.Items.Add(result);
                        }
                        resultCounter++;
                    }
                }
            }
        }

        private void MediaSeachButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            SubFiltersPanel.Visible = false;
            SearchUserPanel.Visible = false;
            SearchArtistPanel.Visible = false;
            SearchMainPanel.Visible = true;
            SubMediaSearchPanel.Visible = false;
            MultifiltrolistBox1.Items.Clear();
            MultiFiltrotextBox1.ResetText();
            SearchMediatextBox.ResetText();
            SearchMediapanellistBox.ResetText();
            if (SearchMediapanel.Visible)
            {
                SearchMediapanel.Visible = true;
                FilterONlable.ResetText();
            }
            else
            {
                SearchMediapanel.Visible = true;
            }
        }
        private void Noresultsongorvideo()
        {
            SearchMediapanellistBox.Items.Add("No results for search criteria");
        }
        private void CleanSearchsongorvideo()
        {
            resultCounter = 0;
            SearchMediapanellistBox.Items.Clear();
        }

        private void SearchMediatextBox_TextChanged(object sender, EventArgs e)
        {
            SerializeData();
            string searchtext = SearchMediatextBox.Text;
            List<string> results = new List<string>();
            if (searchtext.Length >= 1)
            {
                Noresultsongorvideo();
                CleanSearchsongorvideo();
                if (Searchingnamevideoorsong != null)
                {
                    Searchingnamevideoorsong(this, new SearchingSongorVideo() { SearchTextSongVideo = searchtext });
                }

            }
        }
        public void UpdateResultsvideoandsong(List<string> results)
        {
            SerializeData();
            if (results.Count > 0)
            {
                foreach (string result in results)
                {
                    if (resultCounter <= 50)
                    {
                        if (SearchMediapanellistBox.Items.Count > 0 && SearchMediapanellistBox.Items[0].Equals("No results for search criteria"))
                        {
                            SearchMediapanellistBox.Items.Add(result);
                            SearchMediapanellistBox.Items.RemoveAt(0);
                        }
                        else
                            SearchMediapanellistBox.Items.Add(result);
                        resultCounter++;
                    }
                }
            }
        }

        private void UploadSongButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            UploadVideoPanel.Visible = false;
            SongUploadPanel.Visible = true;
            ArtistModeMainPanel.Visible = true;
            AlbumArtistPanel.Visible = false;
            SongCategoriaInput.ResetText();
            SongGenderInput.ResetText();
            SongDiscografiaInput.ResetText();
            SongLetraInput.ResetText();
            SongStudioInput.ResetText();
            SongDuracionTextbox.ResetText();

        }

        private void FiltersButton_Click(object sender, EventArgs e)
        {
            SearchMediatextBox.Clear();
            SearchMediapanellistBox.Items.Clear();
            SerializeData();
            MultifiltroPanel.Visible = false;
            FilterONlable.ResetText();
            if (SubFiltersPanel.Visible)
            {
                SubFiltersPanel.Visible = false;
                FiltroPanel.Visible = false;
            }
            else
            {
                SubFiltersPanel.Visible = true;
                FiltroPanel.Visible = true;
            }
        }
        private void FilterButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            Button button = (Button)sender;
            FilterONlable.ResetText();
            FilterONlable.Text = button.Text;
        }

        private void ArtistSeachButton_Click(object sender, EventArgs e)
        {
            SearchArtistListBox.Items.Clear();
            SearchTextBox.ResetText();
            SerializeData();
            // MainScreenPanel.Visible = false;
            SearchUserPanel.Visible = false;
            SearchMediapanel.Visible = false;
            SearchArtistPanel.Visible = true;
            SearchMainPanel.Visible = true;
            AlbumSearchArtistListbox.Visible = false;

        }

        private void MySongplaylistButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            MySongsListBox.Items.Clear();
            PlaylistMySongPanel.Visible = true;
            MasEsuchadaPanel.Visible = false;
            FollowPlaylistSongPanel.Visible = false;
            SongsInMyPlaylistPanel.Visible = false;
            SongSeguidasPlaylistPanel.Visible = false;
            CrearSongPlaylistPanel.Visible = false;
            queuecheck = true;
            if (SubMyPlaylistPanel.Visible)
            {
                SubMyPlaylistPanel.Visible = false;
            }
            else
            {
                SubMyPlaylistPanel.Visible = true;

            }

            foreach (PlaylistSong playlist in OnReciveUsernamePlaylist())
            {
                if (!MySongsListBox.Items.Contains(playlist.GetPlaylistName()))
                {
                    MySongsListBox.Items.Add(playlist.GetPlaylistName());// con esto accedo al listbox de playlistsong y obtengo las playlist
                }
            }
        }

        public List<PlaylistSong> OnReciveUsernamePlaylist()
        {
            if (Sendingplaylist != null)
            {
                List<PlaylistSong> Userplaylist = Sendingplaylist(this, new GetUserPlaylistEventsArgs() { ActualLoggedUsername = nameuser });
                return Userplaylist;
            }
            return null;
        }
        public List<PlaylistVideo> OnReciveUsernamePlaylistVideo()
        {
            if (Sendingplaylist != null)
            {
                List<PlaylistVideo> Userplaylist = SendingplaylistVideo(this, new GetUserPlaylistEventsArgs() { ActualLoggedUsername = nameuser });
                return Userplaylist;
            }
            return null;
        }

        public List<PlaylistSong> OnReciveUsernameFollowedPlaylist()
        {
            if (Sendingplaylist != null)
            {
                List<PlaylistSong> Userplaylist = Sendingfollowedplaylist(this, new GetUserPlaylistEventsArgs() { ActualLoggedUsername = nameuser });
                return Userplaylist;
            }
            return null;
        }
        public List<PlaylistVideo> OnReciveUsernameFollowedPlaylistVideo()
        {
            if (Sendingplaylist != null)
            {
                List<PlaylistVideo> Userplaylist = Sendingfollowedplaylistvideo(this, new GetUserPlaylistEventsArgs() { ActualLoggedUsername = nameuser });
                return Userplaylist;
            }
            return null;
        }


        private void FollowingPlaylist_Click(object sender, EventArgs e)
        {
            SerializeData();
            FollowPlaylistSongPanel.Visible = true;
            PlaylistMySongPanel.Visible = false;
            CrearSongPlaylistPanel.Visible = false;
            SubMyPlaylistPanel.Visible = false;
            queuecheck = true;

            foreach (PlaylistSong playlist in OnReciveUsernameFollowedPlaylist())
            {
                if (!FollowPlaylistSongListBox.Items.Contains(playlist.GetPlaylistName()))
                {
                    FollowPlaylistSongListBox.Items.Add(playlist.GetPlaylistName());// con esto accedo al listbox de playlistsong y obtengo las playlist
                }
            }
        }

        private void MostLisentSonButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            MasEsuchadaPanel.Visible = true;
            FollowPlaylistSongPanel.Visible = false;
            PlaylistMySongPanel.Visible = false;
            CrearSongPlaylistPanel.Visible = false;
            SubMyPlaylistPanel.Visible = false;
        }

        private void SongButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            PlaylistMainPanel.Visible = true;
            PlaylistSongPanel.Visible = true;
            PlaylistVideoPanel.Visible = false;
            PlaylistMySongPanel.Visible = false;
            FollowPlaylistSongPanel.Visible = false;
            MasEsuchadaPanel.Visible = false;
            SubMyPlaylistPanel.Visible = false;
            queuecheck = true;


        }

        private void VideoButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            PlaylistSongPanel.Visible = false;
            SubVideoPlaylistPanel.Visible = false;
            PlaylistMainPanel.Visible = true;
            PlaylistVideoPanel.Visible = true;
            VideoFollowPanel.Visible = false;
            MasVistosPanel.Visible = false;
            VideoMyPlaylistPanel.Visible = false;
            queuecheck = true;
        }

        private void MyVideoPlaylistButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            MyVideoListBox.Items.Clear();
            VideoMyPlaylistPanel.Visible = true;
            VideoFollowPanel.Visible = false;
            MasVistosPanel.Visible = false;
            MyVideoPlaylistPanel.Visible = false;
            CrearVideoPlaylistpanel.Visible = false;
            queuecheck = true;
            if (SubVideoPlaylistPanel.Visible)
            {
                SubVideoPlaylistPanel.Visible = false;
            }
            else
            {
                SubVideoPlaylistPanel.Visible = true;

            }
            foreach (PlaylistVideo playlist in OnReciveUsernamePlaylistVideo())
            {
                if (!MyVideoListBox.Items.Contains(playlist.GetPlaylistName()))
                {
                    MyVideoListBox.Items.Add(playlist.GetPlaylistName());// con esto accedo al listbox de playlistsong y obtengo las playlist
                }
            }
        }

        private void UploadVideoButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            ArtistModeMainPanel.Visible = true;
            UploadVideoPanel.Visible = true;
            SongUploadPanel.Visible = false;
            AlbumArtistPanel.Visible = false;
            VideoCategoriaTextbox.ResetText();
            VideoGeneroTextBox.ResetText();
            VideoEstudiTextbox.ResetText();
            VideoDescripcionTextBox.ResetText();
            VideoDuracionTextbox.ResetText();
            VideoResolucionCombobox.SelectedIndex = 0;

        }

        private void SongAlbumButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            SongsAlbumPanel.Visible = true;
            VideoAlbumPanel.Visible = false;
            AlbumCanciones.Items.Clear();

            if (Totalitsofsongs != null)
            {
                List<string> listasdelartista = Totalitsofsongs(this, new SendingSongsEventArgs() { Sendinguser = nameuser });
                foreach (string songs in listasdelartista)
                {
                    if (!AlbumCanciones.Items.Contains(songs))
                    {
                        AlbumCanciones.Items.Add(songs);
                    }

                }

            }

        }

        private void VideosAlbumButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            VideoAlbumListBox.Items.Clear();
            SongsAlbumPanel.Visible = false;
            VideoAlbumPanel.Visible = true;
            if (Totalitsofvideos != null)
            {
                List<string> listasdelartista = Totalitsofvideos(this, new SendingVideosEventArgs() { Sendinguser = nameuser });
                foreach (string videos in listasdelartista)
                {
                    if (!VideoAlbumListBox.Items.Contains(videos))
                    {
                        VideoAlbumListBox.Items.Add(videos);
                    }

                }
            }

        }

        private void AlbumButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            AlbumArtistPanel.Visible = true;
            ArtistModeMainPanel.Visible = true;
            UploadVideoPanel.Visible = false;
            SongUploadPanel.Visible = false;
            SongsAlbumPanel.Visible = false;
            VideoAlbumPanel.Visible = false;
        }


        private void MasVistoButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            MasVistosPanel.Visible = true;
            VideoMyPlaylistPanel.Visible = false;
            VideoFollowPanel.Visible = false;
            MyVideoPlaylistPanel.Visible = false;
            SubVideoPlaylistPanel.Visible = false;
        }

        private void FolloweVideoButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            VideoFollowPanel.Visible = true;
            MasVistosPanel.Visible = false;
            VideoMyPlaylistPanel.Visible = false;
            MyVideoPlaylistPanel.Visible = false;
            VideosInFollowingPlaylistPanel.Visible = false;
            SubVideoPlaylistPanel.Visible = false;
            foreach (PlaylistVideo playlist in OnReciveUsernameFollowedPlaylistVideo())
            {
                if (!FollowVideoListBox.Items.Contains(playlist.GetPlaylistName()))
                {
                    FollowVideoListBox.Items.Add(playlist.GetPlaylistName());// con esto accedo al listbox de playlistsong y obtengo las playlist
                }
            }
        }

        private void EditeProfilebutton_Click(object sender, EventArgs e)
        {
            SerializeData();
            ProfileMainPanel.Visible = true;
            EditeProfilePanel.Visible = true;
            CambiarContraseñaPanel.Visible = false;
            CuentaPanel.Visible = false;
            MiInformacionPanel.Visible = false;

        }

        private void InfoProfileButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            ProfileMainPanel.Visible = true;
            EditeProfilePanel.Visible = false;
            MiInformacionPanel.Visible = true;
            UserNameInfoInput.Text = nameuser;
            SeguidoresPanel.Visible = false;
            SeguidosPanel.Visible = false;
            ImagePanel.Visible = false;
        }

        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            CambiarContraseñaPanel.Visible = true;
            CuentaPanel.Visible = false;
            ImagePanel.Visible = false;

        }


        private void CuentaButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            CambiarContraseñaPanel.Visible = false;
            CuentaPanel.Visible = true;
            InfomacionCuentaCambiadaLabel.Visible = false;
            ImagePanel.Visible = false;
        }
        private void CambiarFotoButton_Click(object sender, EventArgs e)
        {
            CambiarContraseñaPanel.Visible = false;
            CuentaPanel.Visible = false;
            ImagePanel.Visible = true;
            SerializeData();
        }

        private void TipoDeCuentaCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GeneroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AceptarCambioCuenta_Click(object sender, EventArgs e)
        {
            string typeAccounte = Convert.ToString(this.TipoDeCuentaCombobox.SelectedItem);
            GenderAccounte = Convert.ToString(this.GeneroComboBox.SelectedItem);
            ageAcctounte = Convert.ToString(this.EdadCuentaInput.Text);
            string privacidad = Convert.ToString(this.PrivacidadInputCuenta.SelectedItem);

            if (Userifosend != null)
            {
                Userifosend(this, new SendingtypeaccountEventArgs() { Usernametext = nameuser, Typeaccount = typeAccounte, Genero = GenderAccounte, Agetext = ageAcctounte, Privacidad = privacidad });
            }
            InfomacionCuentaCambiadaLabel.Visible = true;

            if (ageAcctounte != "")
            {
                EdadCuentaInput.ReadOnly = true;
            }

            if (typeAccounte == "Premium")
            {
                VeryfyArtistPanel.Visible = true;
                NotPrimiumLabelArtist.Visible = false;
                TipoArtistacomboBox1.Visible = true;
                TipoArtistacomboBox1.SelectedIndex = 0;
                TipodeArtistaModeLabel.Visible = true;
                TipoArtistaButton.Visible = true;
                prim = true;
            }
            else
            {
                VeryfyArtistPanel.Visible = true;
                NotPrimiumLabelArtist.Visible = true;
                TipoArtistacomboBox1.Visible = false;
                TipodeArtistaModeLabel.Visible = false;
                TipoArtistaButton.Visible = false;
            }
            SerializeData();

        }


        private void FollowersButton_Click(object sender, EventArgs e)
        {
            SeguidoreslistBox.Items.Clear();
            List<User> followersuser = ShowFollowedUsers(this, new GetUserPlaylistEventsArgs() { ActualLoggedUsername = nameuser });
            foreach (User user in followersuser)
            {
                SeguidoreslistBox.Items.Add(user.Username);
            }
            SeguidoresPanel.Visible = true;
            SeguidosPanel.Visible = false;
            SerializeData();
        }

        private void FollowButton_Click(object sender, EventArgs e)
        {
            SeguidosListBox.Items.Clear();
            List<User> followinguser = ShowFollowingUsers(this, new GetUserPlaylistEventsArgs() { ActualLoggedUsername = nameuser });
            SeguidosListBox.Items.Add("---Usuarios---");
            foreach (User user in followinguser)
            {
                SeguidosListBox.Items.Add(user.Username);
            }
            User logeduser = Userrequest(this, new LoginEventArgs() { UsernameText = nameuser });
            SeguidosListBox.Items.Add("---Artistas---");
            foreach (Artist artist in logeduser.GetFollowedArtist())
            {
                SeguidosListBox.Items.Add(artist.Name);
            }
            SeguidoresPanel.Visible = false;
            SeguidosPanel.Visible = true;
            SerializeData();
        }

        private void PlayButton_Click_1(object sender, EventArgs e)
        {
            HideSubPanel();
            CalificacionComboBox.SelectedIndex = 0;
            ReproduccionMainPanel.Visible = true;
            QueuePanel.Visible = false;
            queuecheck = false;
            if (pasua)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();

            }
            else
            {
                if (namesong != "" && namevideo == "")
                {
                    axWindowsMediaPlayer1.URL = namesong;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    if (Reproduccionesname != null)
                    {
                        Reproduccionesname(this, new ReproduccionesEventArgs() { Nametext = namesong });
                    }
                    pasua = true;
                }
                else if (namesong == "" && namevideo != "")
                {
                    axWindowsMediaPlayer1.URL = namevideo;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    if (Reproduccionesname != null)
                    {
                        Reproduccionesname(this, new ReproduccionesEventArgs() { Nametext = namevideo });

                    }
                    pasua = true;

                }
                if (songqueuelist != null)
                {
                    if (songqueuelist.Count() != 0)
                    {
                        QueueListBox.Items.Add("---Canciones---");
                        foreach (Song song in songqueuelist)
                        {
                            if (song != null)
                            {
                                QueueListBox.Items.Add(song.Namesong);
                            }
                        }
                    }
                }
                if (videoqueuelist != null)
                {
                    if (videoqueuelist.Count() != 0 && videoqueuelist != null)
                    {
                        QueueListBox.Items.Add("---Videos---");
                        foreach (Video video in videoqueuelist)
                        {
                            if (video != null)
                            {
                                QueueListBox.Items.Add(video.VideoName);
                            }
                        }
                    }
                }
            }
            SerializeData();
        }

        private void QueueButton_Click(object sender, EventArgs e)
        {
            SerializeData();
            QueueListBox.Items.Clear();

            if (songqueuelist != null)
            {
                QueueListBox.Items.Add("---Canciones---");
                foreach (Song song in songqueuelist)
                {
                    if (song != null)
                    {
                        QueueListBox.Items.Add(song.Namesong);
                    }
                }
            }

            if (videoqueuelist != null)
            {
                QueueListBox.Items.Add("---Videos---");
                foreach (Video video in videoqueuelist)
                {
                    if (video != null)
                    {
                        QueueListBox.Items.Add(video.VideoName);
                    }
                }
            }

            if (QueuePanel.Visible)
            {
                QueuePanel.Visible = false;
            }
            else
            {
                QueuePanel.Visible = true;

            }
        }

        private void SearchMediapanellistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerializeData();
            if (SearchMediapanellistBox.SelectedItem != null)
            {
                if (Reproducevideo != null)
                {
                    if (!SearchMediapanellistBox.SelectedItem.Equals("No results for search criteria"))
                    {

                        namevideo = Reproducevideo(this, new SelectVideoEventArgs() { Selectedvideo = Convert.ToString(SearchMediapanellistBox.SelectedItem) });
                        pasua = false;
                    }
                }
                if (Reproducesong != null)
                {
                    if (!SearchMediapanellistBox.SelectedItem.Equals("No results for search criteria"))
                    {
                        namesong = Reproducesong(this, new SelectSongEventArgs() { Selectedsong = Convert.ToString(SearchMediapanellistBox.SelectedItem) });
                        pasua = false;
                    }

                }

                if (Recivingsong != null)
                {
                    if (!SearchMediapanellistBox.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablecancion = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(SearchMediapanellistBox.SelectedItem) });
                        songbeingreproduced = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(SearchMediapanellistBox.SelectedItem) });
                        cancionesdelusuario.Add(variablecancion);
                        pasua = false;
                    }

                }
                if (Recivingvideo != null)
                {
                    if (!SearchMediapanellistBox.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablevideo = Recivingvideo(this, new ReturnVideoEventArgs() { Verifyvideoinvideoofuser = Convert.ToString(SearchMediapanellistBox.SelectedItem) });
                        videosdelusuario.Add(variablevideo);
                        pasua = false;
                    }
                }
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            pasua = true;
        }
        //Profile---------------------------------------------------------------------------------------------------//

        //Cambio contraseña:
        private void ChangePasswordAcepted(object sender, EventArgs e)
        {
            SerializeData();
            string pass = ContraseñaActualInput.Text;
            string newpass = NuevaContraseñainput.Text;
            if (Changingpassword != null)
            {
                bool result = Changingpassword(this, new ChangePasswordEventArgs() { Usertext = nameuser, Passwordtext = pass, NewPasswordtext = newpass });
                if (result)
                {
                    MessageBox.Show("Contraseña cambiada");
                }
                else
                {
                    MessageBox.Show("Error al escribir la contraseña");
                }
            }

        }

        //Mi Informacion
        private void SeguidosListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DejarDeSeguirButton1_Click(object sender, EventArgs e)
        {
            string selecteduser = Convert.ToString(SeguidosListBox.SelectedItem);
            User logeduser = Userrequest(this, new LoginEventArgs() { UsernameText = nameuser });
            Artist selectedartist = getArtist(this, new GetArtistEventArgs() { ArtistName = selecteduser });
            List<User> followinguser = ShowFollowingUsers(this, new GetUserPlaylistEventsArgs() { ActualLoggedUsername = nameuser });

            RemoveFollowingUser(this, new GetUserPlaylistEventsArgs() { ActualLoggedUsername = nameuser, ActualUsernameSelected = selecteduser });
            RemoveFollowedUser(this, new GetUserPlaylistEventsArgs() { ActualLoggedUsername = nameuser, ActualUsernameSelected = selecteduser });
            logeduser.UnFollowArtist(selectedartist);

            SeguidosListBox.Items.Clear();
            SeguidosListBox.Items.Add("---Usuarios---");
            foreach (User user in followinguser)
            {
                SeguidosListBox.Items.Add(user.Username);
            }
            SeguidosListBox.Items.Add("---Artistas---");
            foreach (Artist artist in logeduser.GetFollowedArtist())
            {
                SeguidosListBox.Items.Add(artist.Name);
            }
        }

        private void SeguidoreslistBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Modo Artista ----------------------------------------------------------------------------------//
        //AlbumVideo
        private void VideoAlbumListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (VideoAlbumListBox.SelectedItem != null)
            {
                SerializeData();
                if (Reproducevideo != null)
                {
                    if (!VideoAlbumListBox.SelectedItem.Equals("No results for search criteria"))
                    {
                        namevideo = Reproducevideo(this, new SelectVideoEventArgs() { Selectedvideo = Convert.ToString(VideoAlbumListBox.SelectedItem) });
                        pasua = false;
                    }
                }
                if (Recivingvideo != null)
                {
                    if (!VideoAlbumListBox.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablevideo = Recivingvideo(this, new ReturnVideoEventArgs() { Verifyvideoinvideoofuser = Convert.ToString(VideoAlbumListBox.SelectedItem) });
                        videosdelusuario.Add(variablevideo);
                        pasua = false;
                    }
                }
                if (Reproducesong != null)
                {
                    if (!VideoAlbumListBox.SelectedItem.Equals("No results for search criteria"))
                    {
                        namesong = Reproducesong(this, new SelectSongEventArgs() { Selectedsong = Convert.ToString(VideoAlbumListBox.SelectedItem) });
                        pasua = false;
                    }

                }
                if (Recivingsong != null)
                {
                    if (!VideoAlbumListBox.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablecancion = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(VideoAlbumListBox.SelectedItem) });
                        songbeingreproduced = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(SearchMediapanellistBox.SelectedItem) });
                        cancionesdelusuario.Add(variablecancion);
                        pasua = false;
                    }

                }
            }
        }
        //AlbumCancion
        private void AlbumCanciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerializeData();
            if (AlbumCanciones.SelectedItem != null)
            {
                if (Reproducevideo != null)
                {
                    if (!AlbumCanciones.SelectedItem.Equals("No results for search criteria"))
                    {
                        namevideo = Reproducevideo(this, new SelectVideoEventArgs() { Selectedvideo = Convert.ToString(AlbumCanciones.SelectedItem) });
                        pasua = false;
                    }
                }
                if (Recivingvideo != null)
                {
                    if (!AlbumCanciones.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablevideo = Recivingvideo(this, new ReturnVideoEventArgs() { Verifyvideoinvideoofuser = Convert.ToString(AlbumCanciones.SelectedItem) });
                        videosdelusuario.Add(variablevideo);
                        pasua = false;
                    }
                }
                if (Reproducesong != null)
                {
                    if (!AlbumCanciones.SelectedItem.Equals("No results for search criteria"))
                    {
                        namesong = Reproducesong(this, new SelectSongEventArgs() { Selectedsong = Convert.ToString(AlbumCanciones.SelectedItem) });
                        pasua = false;
                    }

                }
                if (Recivingsong != null)
                {
                    if (!AlbumCanciones.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablecancion = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(AlbumCanciones.SelectedItem) });
                        songbeingreproduced = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(AlbumCanciones.SelectedItem) });
                        cancionesdelusuario.Add(variablecancion);
                        pasua = false;
                    }
                }
            }
        }

        //Subir Video
        private void SubirVideoButton_Click(object sender, EventArgs e)
        {
            string videoCategoria = null;
            string videoGender = null;
            string videoDescripcion = null;
            string videoResolucion = null;
            string videoEtudio = null;
            string videoDuracion = null;
            string path;
            string videoName;
            int count = 0;
            videoDuracion = VideoDuracionTextbox.Text;

            videoDuracion = videoDuracion + " Segundos";

            videoCategoria = VideoCategoriaTextbox.Text;
            if (videoCategoria == "")
            {
                MessageBox.Show("Falta campo categoria");
            }
            else
            {
                count++;
            }
            videoGender = VideoGeneroTextBox.Text;
            if (videoGender == "")
            {
                MessageBox.Show("Falta campo Genero");
            }
            else
            {
                count++;
            }
            videoDescripcion = VideoDescripcionTextBox.Text;
            if (videoDescripcion == "")
            {
                MessageBox.Show("Falta campo descripcion");
            }
            else
            {
                count++;
            }
            videoResolucion = VideoResolucionCombobox.SelectedItem.ToString();
            if (videoResolucion == "---Selecione la Resolución---")
            {
                MessageBox.Show("Falta campo Resolucion");
            }
            else
            {
                count++;
            }
            videoEtudio = VideoEstudiTextbox.Text;
            if (videoEtudio == "")
            {
                MessageBox.Show("Falta campo estudio");

            }
            else
            {
                count++;
            }
            if (count == 5)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Selecione elvideo";
                openFileDialog.Filter = "Video file (*.MP4; *.WEBM;*AVI; *.MPG; *.H264;*.MOV;*.WMV;)| *.MP4; *.WEBM;*AVI; *.MPG; *.H264;*.MOV;*.WMV;";

                bool exist = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    videoName = openFileDialog.SafeFileName;
                    path = openFileDialog.FileName;
                    var fileInfo = new FileInfo(path);
                    long tamaño = fileInfo.Length;
                    string videoBytes = tamaño.ToString() + "bytes";
                    if (verifyVideoExist != null)
                    {
                        exist = verifyVideoExist(this, new VideosExistEventsArtgs() { VideoNameText = videoName });
                    }
                    if (exist)
                    {
                        MessageBox.Show("Esta video ya existe");
                        VideoCategoriaTextbox.ResetText();
                        VideoGeneroTextBox.ResetText();
                        VideoEstudiTextbox.ResetText();
                        VideoDescripcionTextBox.ResetText();
                        VideoDuracionTextbox.ResetText();
                        VideoResolucionCombobox.SelectedIndex = 0;
                    }
                    else
                    {
                        String[] separator = { " " };
                        string user_typeartist = Artistwithcaracteristics(this, new LoginEventArgs() { UsernameText = nameuser });
                        String[] username_typeartist = user_typeartist.Split(separator, StringSplitOptions.RemoveEmptyEntries);//[0] nombre(real) del usuario, [1] tipo de artista, [2] genero, [3] edad
                        if (Videocaracteristics != null)
                        {
                            Videocaracteristics(this, new SendingvideocaracteristicsEventArgs() { Videoname = videoName, Genero = videoGender, Categoria = videoCategoria, Actor = username_typeartist[0], Director = username_typeartist[0], Estudio = videoEtudio, Descripcion = videoDescripcion, Sexo = username_typeartist[2], Edad = username_typeartist[3], Resolution = videoResolucion, path = path, byts = videoBytes, duracion = videoDuracion });
                            MessageBox.Show("Video subido con exito");
                            VideoCategoriaTextbox.ResetText();
                            VideoGeneroTextBox.ResetText();
                            VideoEstudiTextbox.ResetText();
                            VideoDescripcionTextBox.ResetText();
                            VideoDuracionTextbox.ResetText();
                            VideoResolucionCombobox.SelectedIndex = 0;
                        }
                    }
                }
                SerializeData();
            }

        }

        // Subir cancion
        private void SubiCancionButton_Click(object sender, EventArgs e)
        {
            string songCategoria = null;
            string songGender = null;
            string songDiscorafia = null;
            string songLetra = null;
            string songEtudio = null;
            string songDuracion = null;
            string path;
            string songName;
            int count = 0;
            songCategoria = SongCategoriaInput.Text;
            songDuracion = SongDuracionTextbox.Text + " Segundos";
            if (songCategoria == "")
            {
                MessageBox.Show("Falta campo categoria");
            }
            else
            {
                count++;
            }
            songGender = SongGenderInput.Text;
            if (songGender == "")
            {
                MessageBox.Show("Falta campo genero");
            }
            else
            {
                count++;
            }
            songDiscorafia = SongDiscografiaInput.Text;
            if (songDiscorafia == "")
            {
                MessageBox.Show("Falta campo discografia");
            }
            else
            {
                count++;
            }
            songLetra = SongLetraInput.Text;
            if (songLetra == "")
            {
                MessageBox.Show("Falta campo letra");
            }
            else
            {
                count++;
            }
            songEtudio = SongStudioInput.Text;
            if (songEtudio == "")
            {
                MessageBox.Show("Falta campo estudio");
            }
            else
            {
                count++;
            }
            if (count == 5)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Selecione la cancion";
                openFileDialog.Filter = "Song file (*.MP3; *.mp3; *.Vorbis; *.Musepack; *.AAC; *.WMA; *.Opus;)|*.MP3; *.mp3 *.Vorbis; *.Musepack; *.AAC; *.WMA; *.Opus;";
                bool exist = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    songName = openFileDialog.SafeFileName;
                    if (verfyedsong != null)
                    {
                        exist = verfyedsong(this, new SongsExistEventsArtgs() { SongName = songName });
                    }
                    if (exist)
                    {
                        MessageBox.Show("Esta cancion ya existe");
                        SongCategoriaInput.ResetText();
                        SongGenderInput.ResetText();
                        SongDiscografiaInput.ResetText();
                        SongLetraInput.ResetText();
                        SongStudioInput.ResetText();
                        SongDuracionTextbox.ResetText();
                    }
                    else
                    {


                        path = openFileDialog.FileName;
                        var fileInfo = new FileInfo(path);
                        long tamaño = fileInfo.Length;
                        string songbytes = tamaño.ToString() + "bytes";
                        String[] separator = { " " };
                        string user_typeartist = Artistwithcaracteristics(this, new LoginEventArgs() { UsernameText = nameuser });
                        String[] username_typeartist = user_typeartist.Split(separator, StringSplitOptions.RemoveEmptyEntries); //[0] nombre(real) del usuario, [1] tipo de artista, [2] genero, [3] edad
                        if (Songcaracteristics != null)
                        {
                            try
                            {
                                Songcaracteristics(this, new SendingsongcaracteristicsEventArgs() { Nombrecancion = songName, Genero = songGender, Compositor = username_typeartist[0], Discografia = songDiscorafia, Estudio = songEtudio, Letra = songLetra, Sexo = username_typeartist[2], Edad = username_typeartist[3], Categoria = songCategoria, path = path, byts = songbytes, duracion = songDuracion });
                                MessageBox.Show("Cancion subida con exito");
                                SongCategoriaInput.ResetText();
                                SongGenderInput.ResetText();
                                SongDiscografiaInput.ResetText();
                                SongLetraInput.ResetText();
                                SongStudioInput.ResetText();
                                SongDuracionTextbox.ResetText();
                            }
                            catch
                            {
                                MessageBox.Show("Te faltan datos por rellenar en tu cuenta");
                            }
                        }
                    }
                }
            }



            SerializeData();

        }
        //Playlist------------------------------------------------------------------------------------------------/

        //Videos
        private void MyVideoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MisVideoMyPlaylist.Items.Clear();
            string playlist_seleccionada = Convert.ToString(MyVideoListBox.SelectedItem);
            MyVideoPlaylistNameLabel.Text = playlist_seleccionada;
            foreach (PlaylistVideo playlist in OnReciveUsernamePlaylistVideo())
            {
                if (playlist.GetPlaylistName() == playlist_seleccionada)
                {
                    foreach (Video videos in playlist.GetPlaylistAllVideos())
                    {
                        if (videos != null)
                        {
                            MisVideoMyPlaylist.Items.Add(videos.VideoName);
                        }
                    }
                }
            }
            SerializeData();
        }

        private void FollowVideoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            VideosInFollowingPlaylistListbox.Items.Clear();
            string playlist_seleccionada = Convert.ToString(FollowVideoListBox.SelectedItem);
            VideoFollowingPlaylistName.Text = playlist_seleccionada;
            foreach (PlaylistVideo playlist in OnReciveUsernameFollowedPlaylistVideo())
            {
                if (playlist.GetPlaylistName() == playlist_seleccionada)
                {
                    foreach (Video videos in playlist.GetPlaylistAllVideos())
                    {
                        if (videos != null)
                        {
                            VideosInFollowingPlaylistListbox.Items.Add(videos.VideoName);
                        }
                    }
                }
            }
        }

        private void VideosMasVistos_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerializeData();
            if (VideosMasVistos.SelectedItem != null)
            {
                if (Reproducevideo != null)
                {
                    if (!VideosMasVistos.SelectedItem.Equals("No results for search criteria"))
                    {
                        namevideo = Reproducevideo(this, new SelectVideoEventArgs() { Selectedvideo = Convert.ToString(VideosMasVistos.SelectedItem) });
                        pasua = false;
                    }
                }
                if (Recivingvideo != null)
                {
                    if (!VideosMasVistos.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablevideo = Recivingvideo(this, new ReturnVideoEventArgs() { Verifyvideoinvideoofuser = Convert.ToString(VideosMasVistos.SelectedItem) });
                        videosdelusuario.Add(variablevideo);
                        pasua = false;
                    }
                }
                if (Reproducesong != null)
                {
                    if (!VideosMasVistos.SelectedItem.Equals("No results for search criteria"))
                    {
                        namesong = Reproducesong(this, new SelectSongEventArgs() { Selectedsong = Convert.ToString(VideosMasVistos.SelectedItem) });
                        pasua = false;
                    }

                }
                if (Recivingsong != null)
                {
                    if (!VideosMasVistos.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablecancion = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(VideosMasVistos.SelectedItem) });
                        songbeingreproduced = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(VideosMasVistos.SelectedItem) });
                        cancionesdelusuario.Add(variablecancion);
                        pasua = false;
                    }
                }
            }
        }

        private void AddPlaylistVideoButton_Click(object sender, EventArgs e)
        {
            Errorplaylistvideo.Visible = false;
            NombrePlaylistExiste.Visible = false;
            VideoPlaylistCreadaConExitoLabel.Visible = false;
            CrearVideoPlaylistpanel.Visible = true;
            PrivacidadVideoPlaylist.SelectedIndex = 1;
            SerializeData();
            NombreVideoPlalistInput.ResetText();
        }


        private void DeleteVideoPlaylistButton_Click(object sender, EventArgs e)
        {
            buttonClickdelete = true;
            string playlist_seleccionada = Convert.ToString(MyVideoListBox.SelectedItem);
            if (Userselectedvideoplaylist != null)
            {

                if (Userselectedvideoplaylist(this, new GetUserPlaylistEventsArgs { ActualPlaylistSelected = playlist_seleccionada, ActualLoggedUsername = nameuser }) && buttonClickdelete)
                {
                    MyVideoListBox.Items.Remove(playlist_seleccionada);
                }
            }
            MyVideoListBox.Items.Clear();
            foreach (PlaylistVideo playlist in OnReciveUsernamePlaylistVideo())
            {
                if (!MyVideoListBox.Items.Contains(playlist.GetPlaylistName()))
                {
                    MyVideoListBox.Items.Add(playlist.GetPlaylistName());// con esto accedo al listbox de playlistsong y obtengo las playlist
                }
            }
        }

        //Canciones
        private void MySongsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SongInMyPlaylistListBox.Items.Clear();
            string playlist_seleccionada = Convert.ToString(MySongsListBox.SelectedItem);
            NombreMyplaylistSonglabel.Text = playlist_seleccionada;
            foreach (PlaylistSong playlist in OnReciveUsernamePlaylist())
            {
                if (playlist.GetPlaylistName() == playlist_seleccionada)
                {
                    foreach (Song canciones in playlist.GetPlaylistAllSongs())
                    {
                        if (canciones != null)
                        {
                            SongInMyPlaylistListBox.Items.Add(canciones.Namesong);
                        }
                    }
                }
            }
            SerializeData();
        }

        private void FollowPlaylistSongListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SongsInFollowPlaylistListBox.Items.Clear();
            string playlist_seleccionada = Convert.ToString(FollowPlaylistSongListBox.SelectedItem);
            NombrePlaylistSeguidaLabel.Text = playlist_seleccionada;
            foreach (PlaylistSong playlist in OnReciveUsernameFollowedPlaylist())
            {
                if (playlist.GetPlaylistName() == playlist_seleccionada)
                {
                    foreach (Song song in playlist.GetPlaylistAllSongs())
                    {
                        if (song != null)
                        {
                            SongsInFollowPlaylistListBox.Items.Add(song.Namesong);
                        }
                    }
                }
            }
        }

        private void MasEscuchadaListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddPlaylistButton_Click(object sender, EventArgs e)
        {
            CrearSongPlaylistPanel.Visible = true;
            ErrorCuentaPrivadaPlaylistSong.Visible = false;
            ErrorExisteSongPlaylisteNombre.Visible = false;
            PlaylistSongCreada.Visible = false;
            NewSongPrivacidadComboBox.SelectedIndex = 1;
            SerializeData();
            PlaylistSongNameInput.ResetText();
        }

        private void DeletePlaylistButton_Click(object sender, EventArgs e)
        {
            buttonClickdelete = true;
            string playlist_seleccionada = Convert.ToString(MySongsListBox.SelectedItem);
            if (Userselectedplaylist != null)
            {
                if (Userselectedplaylist(this, new GetUserPlaylistEventsArgs { ActualPlaylistSelected = playlist_seleccionada, ActualLoggedUsername = nameuser }) && buttonClickdelete)
                {
                    MySongsListBox.Items.Remove(playlist_seleccionada);
                }
            }
            MySongsListBox.Items.Clear();
            foreach (PlaylistSong playlist in OnReciveUsernamePlaylist())
            {
                if (!MySongsListBox.Items.Contains(playlist.GetPlaylistName()))
                {
                    MySongsListBox.Items.Add(playlist.GetPlaylistName());// con esto accedo al listbox de playlistsong y obtengo las playlist
                }
            }
            SerializeData();
        }

        private void VerCancionesEnMisPlaylistButton_Click(object sender, EventArgs e)
        {
            SongsInMyPlaylistPanel.Visible = true;
            SerializeData();
        }
        private void BackMyPlaylistSong_Click(object sender, EventArgs e)
        {
            SongsInMyPlaylistPanel.Visible = false;
            SerializeData();
        }

        private void SongInMyPlaylistListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SongInMyPlaylistListBox.SelectedItem != null)
            {
                if (Reproducevideo != null)
                {
                    if (!SongInMyPlaylistListBox.SelectedItem.Equals("No results for search criteria"))
                    {
                        if (queuecheck == true)
                        {
                            string actualplaylistselected = MySongsListBox.SelectedItem.ToString();
                            string actualselectedsong = SongInMyPlaylistListBox.SelectedItem.ToString();
                        }
                        pasua = false;
                        namevideo = Reproducevideo(this, new SelectVideoEventArgs() { Selectedvideo = Convert.ToString(SongInMyPlaylistListBox.SelectedItem) });
                        SerializeData();
                    }
                }
                if (Reproducesong != null)
                {
                    if (!SongInMyPlaylistListBox.SelectedItem.Equals("No results for search criteria"))
                    {
                        if (queuecheck == true)
                        {
                            string actualplaylistselected = MySongsListBox.SelectedItem.ToString();
                            string actualselectedsong = SongInMyPlaylistListBox.SelectedItem.ToString();
                            songqueuelist = CreateSongQueue(this, new GetUserPlaylistEventsArgs { ActualLoggedUsername = nameuser, ActualPlaylistSelected = actualplaylistselected, SelectedSong = actualselectedsong });
                            bool emptyqueue = false;
                            if (videoqueuelist != null)
                            {
                                videoqueuelist.Clear();
                            }
                        }
                        namesong = Reproducesong(this, new SelectSongEventArgs() { Selectedsong = Convert.ToString(SongInMyPlaylistListBox.SelectedItem) });
                        pasua = false;
                    }
                }

                if (Recivingsong != null)
                {
                    if (!SongInMyPlaylistListBox.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablecancion = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(SongInMyPlaylistListBox.SelectedItem) });
                        songbeingreproduced = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(SongInMyPlaylistListBox.SelectedItem) });
                        cancionesdelusuario.Add(variablecancion);
                        pasua = false;
                    }
                }
                if (Recivingvideo != null)
                {
                    if (!SongInMyPlaylistListBox.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablevideo = Recivingvideo(this, new ReturnVideoEventArgs() { Verifyvideoinvideoofuser = Convert.ToString(SongInMyPlaylistListBox.SelectedItem) });
                        videosdelusuario.Add(variablevideo);
                        pasua = false;
                    }
                }
            }
        }



        private void VerCancionesPlaylistSeguidas_Click(object sender, EventArgs e)
        {
            SongSeguidasPlaylistPanel.Visible = true;
            SerializeData();
        }

        private void BackPlaylistSeguidas_Click(object sender, EventArgs e)
        {
            SongSeguidasPlaylistPanel.Visible = false;
            SerializeData();
        }

        private void SongsInFollowPlaylistListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Reproducevideo != null)
            {
                if (!SongsInFollowPlaylistListBox.SelectedItem.Equals("No results for search criteria"))
                {
                    if (queuecheck == true)
                    {
                        string actualplaylistselected = FollowPlaylistSongListBox.SelectedItem.ToString();
                        string actualselectedsong = SongsInFollowPlaylistListBox.SelectedItem.ToString();
                    }
                    pasua = false;
                    namevideo = Reproducevideo(this, new SelectVideoEventArgs() { Selectedvideo = Convert.ToString(SongsInFollowPlaylistListBox.SelectedItem) });
                    SerializeData();
                }
            }
            if (Reproducesong != null)
            {
                if (SongsInFollowPlaylistListBox.SelectedItem != null)
                {
                    if (!SongsInFollowPlaylistListBox.SelectedItem.Equals("No results for search criteria"))
                    {
                        if (queuecheck == true)
                        {
                            string actualplaylistselected = FollowPlaylistSongListBox.SelectedItem.ToString();
                            string actualselectedsong = SongsInFollowPlaylistListBox.SelectedItem.ToString();
                            songqueuelist = CreateSongQueue(this, new GetUserPlaylistEventsArgs { ActualLoggedUsername = nameuser, ActualPlaylistSelected = actualplaylistselected, SelectedSong = actualselectedsong });
                            bool emptyqueue = false;
                            if (videoqueuelist != null)
                            {
                                videoqueuelist.Clear();
                            }
                        }
                        namesong = Reproducesong(this, new SelectSongEventArgs() { Selectedsong = Convert.ToString(SongsInFollowPlaylistListBox.SelectedItem) });
                        pasua = false;
                    }

                }

                if (Recivingsong != null)
                {
                    if (!SongsInFollowPlaylistListBox.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablecancion = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(SongsInFollowPlaylistListBox.SelectedItem) });
                        songbeingreproduced = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(SongsInFollowPlaylistListBox.SelectedItem) });
                        cancionesdelusuario.Add(variablecancion);
                        pasua = false;
                        SerializeData();
                    }
                }
                if (Recivingvideo != null)
                {
                    if (!SongsInFollowPlaylistListBox.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablevideo = Recivingvideo(this, new ReturnVideoEventArgs() { Verifyvideoinvideoofuser = Convert.ToString(SongsInFollowPlaylistListBox.SelectedItem) });
                        videosdelusuario.Add(variablevideo);
                        pasua = false;
                    }
                }
            }
        }


        private void AddSongMyPlaylists_Click(object sender, EventArgs e)
        {
            string playlist_seleccionada = Convert.ToString(MySongsListBox.SelectedItem);
            string songseleccionda = Convert.ToString(SongInMyPlaylistListBox.SelectedItem);

            foreach (PlaylistSong cancionesinplaylistsong in OnReciveUsernamePlaylist())
            {
                if (cancionesinplaylistsong.GetPlaylistName() == playlist_seleccionada)
                {
                    try
                    {
                        foreach (Song song in cancionesinplaylistsong.GetPlaylistAllSongs())
                        {
                            if (song.Namesong == songseleccionda)
                            {
                                cancionesinplaylistsong.RemoveSong(song);
                                SongInMyPlaylistListBox.Items.Remove(songseleccionda);
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void BorrarCancionMyplaylist_Click(object sender, EventArgs e)
        {
            string playlist_seleccionada = Convert.ToString(MySongsListBox.SelectedItem);
            string songseleccionda = Convert.ToString(SongInMyPlaylistListBox.SelectedItem);

            foreach (PlaylistSong cancionesinplaylistsong in OnReciveUsernamePlaylist())
            {
                if (cancionesinplaylistsong.GetPlaylistName() == playlist_seleccionada)
                {
                    try
                    {
                        foreach (Song song in cancionesinplaylistsong.GetPlaylistAllSongs())
                        {
                            if (song.Namesong == songseleccionda)
                            {
                                cancionesinplaylistsong.RemoveSong(song);
                                SongInMyPlaylistListBox.Items.Remove(songseleccionda);
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void CrearSongPlaylistButton_Click(object sender, EventArgs e)
        {
            string nameplaylist = PlaylistSongNameInput.Text;
            string privacidad = Convert.ToString(NewSongPrivacidadComboBox.SelectedItem);
            bool UserNotPublic = false;
            bool NamePlaylistExist = false;
            if (Addplaylist != null)
            {
                actuallogeduser = Userrequest(this, new LoginEventArgs() { UsernameText = nameuser });
                if (actuallogeduser.Privacidad == "Privado" && privacidad == "Publica")
                {
                    MessageBox.Show("No puede crer listas publicas si el usuario es privado");
                }
                else
                {
                    Addplaylist(this, new GetUserPlaylistEventsArgs() { PlaylistNameText = nameplaylist, ActualLoggedUsername = nameuser }); //le estoy mandado a usercontroller el nombre de la playlist
                    SetPlaylistSongPrivacy(this, new GetUserPlaylistEventsArgs() { ActualPlaylistSelected = nameplaylist, ActualLoggedUsername = nameuser, UserSelectedPrivacy = privacidad });
                }
            }
            NewSongPrivacidadComboBox.SelectedIndex = 1;
            SerializeData();
            PlaylistSongNameInput.ResetText();

        }

        //Search-----------------------------------------------------------------------------------------------------//

        private void SearchUserPanelResultlistusers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SearchArtistListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string artist = SearchTextBox.Text;
            List<string> results = new List<string>();
            if (artist.Length >= 2)
            {
                CleanSearch();
                Noresult();
                if (Artistinfo != null)
                {
                    Artistinfo(this, new ArtistInfoEventArgs() { ArtistText = artist });
                }

            }


        }
        public void UpdateResultsArtist(List<string> results)
        {
            SerializeData();
            if (results.Count > 0)
            {
                foreach (string result in results)
                {
                    if (resultCounter <= 50)
                    {
                        if (SearchArtistListBox.Items.Count > 0 && SearchArtistListBox.Items[0].Equals("No results for search criteria"))
                        {
                            SearchArtistListBox.Items.Add(result);
                            SearchArtistListBox.Items.RemoveAt(0);

                        }
                        else
                            SearchArtistListBox.Items.Add(result);
                        resultCounter++;
                    }
                }
            }
        }




        //Reproducion------------------------------------------------------------------------------------------------//
        private void CalsificacionButton_Click(object sender, EventArgs e)
        {
            int qual = Convert.ToInt32(CalificacionComboBox.SelectedItem.ToString());
            if (namesong != "")
            {
                if (Calificaciondelusuario != null)
                {
                    Calificaciondelusuario(this, new MandarcalficacionEventArgs() { Calification = qual, Namecancion = namesong });


                }

            }
            if (namevideo != "")
            {
                if (Calificaciondelusuario != null)
                {
                    Calificaciondelusuario(this, new MandarcalficacionEventArgs() { Calification = qual, Namecancion = namevideo });

                }

            }
            CalificacionComboBox.SelectedIndex = 0;

        }

        private void VideoInMyplaylistButton_Click(object sender, EventArgs e)
        {
            MyVideoPlaylistPanel.Visible = true;
        }

        private void BackMyVideoPlaylistButton_Click(object sender, EventArgs e)
        {
            MyVideoPlaylistPanel.Visible = false;
        }

        private void CrearVideoPlaylist_Click(object sender, EventArgs e)
        {

            string nameVideo = NombreVideoPlalistInput.Text;
            string privacidad = Convert.ToString(PrivacidadVideoPlaylist.SelectedItem);

            bool UserNotPublic = false;
            bool NamePlaylistExist = false;

            if (Addvideoplaylist != null)
            {
                Addvideoplaylist(this, new GetUserPlaylistEventsArgs() { PlaylistNameText = nameVideo, ActualLoggedUsername = nameuser }); //le estoy mandado a usercontroller el nombre de la playlist
                SetPlaylistVideoPrivacy(this, new GetUserPlaylistEventsArgs() { ActualPlaylistSelected = nameVideo, ActualLoggedUsername = nameuser, UserSelectedPrivacy = privacidad });
            }

            if (UserNotPublic)
            {
                Errorplaylistvideo.Visible = true;
            }
            if (NamePlaylistExist)
            {
                NombrePlaylistExiste.Visible = true;
            }
            else
            {
                VideoPlaylistCreadaConExitoLabel.Visible = true;
            }
            SerializeData();
            PrivacidadVideoPlaylist.SelectedIndex = 1;
            NombreVideoPlalistInput.ResetText();

        }

        private void BackFollowingPlaylist_Click(object sender, EventArgs e)
        {
            VideosInFollowingPlaylistPanel.Visible = false;
            SerializeData();
        }

        private void VideosInFollowingPlaylistButton_Click(object sender, EventArgs e)
        {
            VideosInFollowingPlaylistPanel.Visible = true;
            SerializeData();
        }

        private void VideosInFollowingPlaylistListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (VideosInFollowingPlaylistListbox.SelectedItem != null)
            {
                if (Reproducevideo != null)
                {
                    if (!VideosInFollowingPlaylistListbox.SelectedItem.Equals("No results for search criteria"))
                    {
                        if (queuecheck == true)
                        {
                            string actualplaylistselected = FollowVideoListBox.SelectedItem.ToString();
                            string actualselectedsong = VideosInFollowingPlaylistListbox.SelectedItem.ToString();
                            videoqueuelist = CreateVideoQueue(this, new GetUserPlaylistEventsArgs { ActualLoggedUsername = nameuser, ActualPlaylistSelected = actualplaylistselected, SelectedSong = actualselectedsong });
                            bool emptyqueue = false;
                            if (songqueuelist != null)
                            {
                                songqueuelist.Clear();
                            }
                        }
                        pasua = false;
                        namevideo = Reproducevideo(this, new SelectVideoEventArgs() { Selectedvideo = Convert.ToString(VideosInFollowingPlaylistListbox.SelectedItem) });
                        SerializeData();
                    }
                }
                if (Reproducesong != null)
                {
                    if (!VideosInFollowingPlaylistListbox.SelectedItem.Equals("No results for search criteria"))
                    {
                        if (queuecheck == true)
                        {
                            string actualplaylistselected = FollowVideoListBox.SelectedItem.ToString();
                            string actualselectedsong = VideosInFollowingPlaylistListbox.SelectedItem.ToString();
                        }
                        namesong = Reproducesong(this, new SelectSongEventArgs() { Selectedsong = Convert.ToString(VideosInFollowingPlaylistListbox.SelectedItem) });
                        pasua = false;
                    }
                }

                if (Recivingsong != null)
                {
                    if (!VideosInFollowingPlaylistListbox.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablecancion = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(VideosInFollowingPlaylistListbox.SelectedItem) });
                        songbeingreproduced = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(VideosInFollowingPlaylistListbox.SelectedItem) });
                        cancionesdelusuario.Add(variablecancion);
                        pasua = false;
                    }
                }

                if (Recivingvideo != null)
                {
                    if (!VideosInFollowingPlaylistListbox.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablevideo = Recivingvideo(this, new ReturnVideoEventArgs() { Verifyvideoinvideoofuser = Convert.ToString(VideosInFollowingPlaylistListbox.SelectedItem) });
                        videosdelusuario.Add(variablevideo);
                        pasua = false;
                    }
                }
            }
        }

        private void MisVideoMyPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MisVideoMyPlaylist.SelectedItem != null)
            {
                if (Reproducevideo != null)
                {
                    if (!MisVideoMyPlaylist.SelectedItem.Equals("No results for search criteria"))
                    {
                        if (queuecheck == true)
                        {
                            string actualplaylistselected = MyVideoListBox.SelectedItem.ToString();
                            string actualselectedsong = MisVideoMyPlaylist.SelectedItem.ToString();
                            videoqueuelist = CreateVideoQueue(this, new GetUserPlaylistEventsArgs { ActualLoggedUsername = nameuser, ActualPlaylistSelected = actualplaylistselected, SelectedSong = actualselectedsong });
                            bool emptyqueue = false;
                            if (songqueuelist != null)
                            {
                                songqueuelist.Clear();
                            }
                        }
                        namevideo = Reproducevideo(this, new SelectVideoEventArgs() { Selectedvideo = Convert.ToString(MisVideoMyPlaylist.SelectedItem) });
                        pasua = false;
                    }
                }

                if (Reproducesong != null)
                {
                    if (!MisVideoMyPlaylist.SelectedItem.Equals("No results for search criteria"))
                    {
                        if (queuecheck == true)
                        {
                            string actualplaylistselected = MyVideoListBox.SelectedItem.ToString();
                            string actualselectedsong = MisVideoMyPlaylist.SelectedItem.ToString();
                        }
                        namesong = Reproducesong(this, new SelectSongEventArgs() { Selectedsong = Convert.ToString(MisVideoMyPlaylist.SelectedItem) });
                        pasua = false;
                    }
                }

                if (Recivingsong != null)
                {
                    if (!MisVideoMyPlaylist.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablecancion = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(MisVideoMyPlaylist.SelectedItem) });
                        songbeingreproduced = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(MisVideoMyPlaylist.SelectedItem) });
                        cancionesdelusuario.Add(variablecancion);
                        pasua = false;
                    }
                }

                if (Recivingvideo != null)
                {
                    if (!MisVideoMyPlaylist.SelectedItem.Equals("No results for search criteria"))
                    {
                        variablevideo = Recivingvideo(this, new ReturnVideoEventArgs() { Verifyvideoinvideoofuser = Convert.ToString(MisVideoMyPlaylist.SelectedItem) });
                        SerializeData();
                        pasua = false;
                    }
                }
            }
        }

        private void AddMediaButton_Click(object sender, EventArgs e)
        {

        }

        //Este metodo es para que apareca el panel del playlist

        private void AddToPlaylistButton_Click(object sender, EventArgs e)
        {
            if (SubMediaSearchPanel.Visible)
            {
                SubMediaSearchPanel.Visible = false;
            }
            else
            {
                SubMediaSearchPanel.Visible = true;

                foreach (PlaylistVideo playlist in OnReciveUsernamePlaylistVideo())
                {
                    if (!PlaylistListBoxAdd.Items.Contains(playlist.GetPlaylistName()))
                    {
                        PlaylistListBoxAdd.Items.Add(playlist.GetPlaylistName());// con esto accedo al listbox de playlistsong y obtengo las playlist
                    }
                }

                foreach (PlaylistSong playlist in OnReciveUsernamePlaylist())
                {
                    if (!PlaylistListBoxAdd.Items.Contains(playlist.GetPlaylistName()))
                    {
                        PlaylistListBoxAdd.Items.Add(playlist.GetPlaylistName());// con esto accedo al listbox de playlistsong y obtengo las playlist
                    }
                }
            }
            SerializeData();
        }

        private void PlaylistListBoxAdd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        bool botonparaagregaralaplaylist = false; //boton que agrega a la playlist

        private void AgregarMediaPlaylistButton_Click(object sender, EventArgs e)
        {
            if (PlaylistListBoxAdd.SelectedItem != null)
            {
                botonparaagregaralaplaylist = true;
                if (!PlaylistListBoxAdd.SelectedItem.Equals("No results for search criteria") && botonparaagregaralaplaylist)
                {
                    bool songflag = true;
                    bool videoflag = true;
                    try
                    {
                        foreach (PlaylistSong cancionesinplaylistsong in OnReciveUsernamePlaylist())
                        {
                            if (cancionesinplaylistsong.GetPlaylistName() == Convert.ToString(PlaylistListBoxAdd.SelectedItem))
                            {
                                variablecancion = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(SearchMediapanellistBox.SelectedItem) });
                                foreach (Song song in cancionesinplaylistsong.GetPlaylistAllSongs())
                                {
                                    if (song.Namesong == variablecancion.Namesong)
                                    {
                                        songflag = false;
                                        MessageBox.Show("Canción ya existe en esta playlist");
                                    }
                                }
                                if (songflag == true)
                                {
                                    cancionesinplaylistsong.AddSong(variablecancion);
                                    MessageBox.Show("Canción agregada a tu playlist");
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        foreach (PlaylistVideo cancionesinplaylistvideo in OnReciveUsernamePlaylistVideo())
                        {
                            if (cancionesinplaylistvideo.GetPlaylistName() == Convert.ToString(PlaylistListBoxAdd.SelectedItem))
                            {
                                variablevideo = Recivingvideo(this, new ReturnVideoEventArgs() { Verifyvideoinvideoofuser = Convert.ToString(SearchMediapanellistBox.SelectedItem) });
                                foreach (Video video in cancionesinplaylistvideo.GetPlaylistAllVideos())
                                {
                                    if (video.VideoName == variablevideo.VideoName)
                                    {
                                        videoflag = false;
                                        MessageBox.Show("Video ya existe en esta playlist");
                                    }
                                }
                                if (videoflag == true)
                                {
                                    cancionesinplaylistvideo.AddVideo(variablevideo);
                                    MessageBox.Show("Video agregado a tu playlist");
                                }
                            }
                        }
                    }
                    catch
                    {
                    }

                    PlaylistListBoxAdd.Items.Clear();

                    foreach (PlaylistVideo playlist in OnReciveUsernamePlaylistVideo())
                    {
                        if (!PlaylistListBoxAdd.Items.Contains(playlist.GetPlaylistName()))
                        {
                            PlaylistListBoxAdd.Items.Add(playlist.GetPlaylistName());// con esto accedo al listbox de playlistsong y obtengo las playlist
                        }
                    }

                    foreach (PlaylistSong playlist in OnReciveUsernamePlaylist())
                    {
                        if (!PlaylistListBoxAdd.Items.Contains(playlist.GetPlaylistName()))
                        {
                            PlaylistListBoxAdd.Items.Add(playlist.GetPlaylistName());// con esto accedo al listbox de playlistsong y obtengo las playlist
                        }
                    }

                }
            }
            SerializeData();
        }
        //Buscar Usuario--------------------------------------------------------------------------------------------------//

        private void FollowUserButton_Click(object sender, EventArgs e)
        {
            string selectedusername = Convert.ToString(SearchUserPanelResultlistusers.SelectedItem);
            AddFollowedUser(this, new GetUserPlaylistEventsArgs { ActualLoggedUsername = nameuser, ActualUsernameSelected = selectedusername });
            AddFollowingUser(this, new GetUserPlaylistEventsArgs { ActualLoggedUsername = nameuser, ActualUsernameSelected = selectedusername });
        }

        public List<PlaylistSong> OnReciveUsernamePlaylist(string selecteduser)
        {
            if (Sendingplaylist != null)
            {
                List<PlaylistSong> Userplaylist = Sendingplaylist(this, new GetUserPlaylistEventsArgs() { ActualLoggedUsername = selecteduser });
                return Userplaylist;
            }
            return null;
        }
        public List<PlaylistVideo> OnReciveUsernamePlaylistVideo(string selecteduser)
        {
            if (Sendingplaylist != null)
            {
                List<PlaylistVideo> Userplaylist = SendingplaylistVideo(this, new GetUserPlaylistEventsArgs() { ActualLoggedUsername = selecteduser });
                return Userplaylist;
            }
            return null;
        }

        private void PlaylistsUserSearchButton_Click(object sender, EventArgs e)
        {

            string selecteduser = Convert.ToString(SearchUserPanelResultlistusers.SelectedItem);

            foreach (PlaylistSong playlist in OnReciveUsernamePlaylist(selecteduser))
            {
                if (!SearchUserPlaylistListbox.Items.Contains(playlist.GetPlaylistName()))
                {
                    if (playlist.GetPlaylistPrivacy() == true)
                    {
                        SearchUserPlaylistListbox.Items.Add(playlist.GetPlaylistName());// con esto accedo al listbox de playlistsong y obtengo las playlist
                    }
                }
            }
            foreach (PlaylistVideo playlist in OnReciveUsernamePlaylistVideo(selecteduser))
            {
                if (!SearchUserPlaylistListbox.Items.Contains(playlist.GetPlaylistName()))
                {
                    if (playlist.GetPlaylistPrivacy() == true)
                    {
                        SearchUserPlaylistListbox.Items.Add(playlist.GetPlaylistName());// con esto accedo al listbox de playlistsong y obtengo las playlist
                    }
                }
            }
            if (SeguirPlaylistPanel.Visible)
            {
                SeguirPlaylistPanel.Visible = false;
            }
            else
            {
                SeguirPlaylistPanel.Visible = true;

            }
            SerializeData();
        }

        private void SearchUserPlaylistListbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FollowPlyalistButton_Click(object sender, EventArgs e)
        {
            string selecteduser = Convert.ToString(SearchUserPanelResultlistusers.SelectedItem);
            string selectedplaylist = Convert.ToString(SearchUserPlaylistListbox.SelectedItem);
            Followmusicplaylist(this, new GetUserPlaylistEventsArgs() { ActualLoggedUsername = nameuser, ActualPlaylistSelected = selectedplaylist, ActualUsernameSelected = selecteduser });

            Followvideoplaylist(this, new GetUserPlaylistEventsArgs() { ActualLoggedUsername = nameuser, ActualPlaylistSelected = selectedplaylist, ActualUsernameSelected = selecteduser });

        }

        private void TipoArtistaButton_Click(object sender, EventArgs e)
        {
            string artist = this.TipoArtistacomboBox1.SelectedItem.ToString();

            if (Artistifosend != null)
            {
                Artistifosend(this, new SendingArtistInfo() { Usernametext = nameuser, ArtistText = artist, AgeArtist = ageAcctounte, GenderArtist = GenderAccounte });
            }

            VeryfyArtistPanel.Visible = false;
            SerializeData();
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Selecione la imagen";
            openFileDialog.Filter = "Image file (*.jpg; *.jpeg;*.bmp;*.PNG;)|*.jpg; *.jpeg;*.bmp;*.PNG;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap image = new Bitmap(openFileDialog.FileName);
                if (changeImage != null)
                {
                    changeImage(this, new ChangeImageEventsArgs() { Usernametext = nameuser, Ipath = openFileDialog.FileName });
                }
                pictureBox1.BackgroundImage = null;
                pictureBox2.BackgroundImage = null;
                pictureBox1.Image = image;
                pictureBox2.Image = image;
            }
        }

        private void VolumenTrackBar1_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = VolumenTrackBar1.Value;
        }


        //----------------------------------------------------------Botones Nuevos--------------------------------------------------------//
        private void DejarSeguirVideoPlaylist_Click(object sender, EventArgs e)
        {
            string playlist_seleccionada = Convert.ToString(FollowVideoListBox.SelectedItem);

            if (Userselectedfollowedvideoplaylist != null)
            {
                Userselectedfollowedvideoplaylist(this, new GetUserPlaylistEventsArgs { ActualPlaylistSelected = playlist_seleccionada, ActualLoggedUsername = nameuser });
                FollowVideoListBox.Items.Remove(playlist_seleccionada);
            }
            SerializeData();
        }

        private void DejarSeguirCancionbutton_Click(object sender, EventArgs e)
        {
            string playlist_seleccionada = Convert.ToString(FollowPlaylistSongListBox.SelectedItem);

            if (Userselectedfollowedplaylist != null)
            {
                Userselectedfollowedplaylist(this, new GetUserPlaylistEventsArgs { ActualPlaylistSelected = playlist_seleccionada, ActualLoggedUsername = nameuser });
                FollowPlaylistSongListBox.Items.Remove(playlist_seleccionada);
            }
            SerializeData();
        }

        private void Album_Click(object sender, EventArgs e)
        {
            if (SearchArtistListBox.SelectedItem != null)
            {
                AlbumSearchArtistListbox.Items.Clear();
                string artistname = SearchArtistListBox.SelectedItem.ToString();
                if (Userrequest != null)
                {

                    if (Totalitsofsongs != null)
                    {
                        AlbumSearchArtistListbox.Items.Add("Canciones");
                        List<string> listasdelartista = Totalitsofsongs(this, new SendingSongsEventArgs() { Sendinguser = artistname });
                        foreach (string songs in listasdelartista)
                        {
                            AlbumSearchArtistListbox.Items.Add(songs);
                        }


                    }
                    if (Totalitsofvideos != null)
                    {
                        AlbumSearchArtistListbox.Items.Add("Videos");
                        List<string> listasdelartista = Totalitsofvideos(this, new SendingVideosEventArgs() { Sendinguser = artistname });
                        foreach (string videos in listasdelartista)
                        {
                            AlbumSearchArtistListbox.Items.Add(videos);
                        }
                    }




                }

                if (AlbumSearchArtistListbox.Visible)
                {
                    AlbumSearchArtistListbox.Visible = false;
                }
                else
                {
                    AlbumSearchArtistListbox.Visible = true;
                }
            }
        }

        private void AlbumSearchArtistListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerializeData();
            if (Reproducevideo != null)
            {
                if (!AlbumSearchArtistListbox.SelectedItem.Equals("Video"))
                {
                    namevideo = Reproducevideo(this, new SelectVideoEventArgs() { Selectedvideo = Convert.ToString(AlbumSearchArtistListbox.SelectedItem) });
                    pasua = false;
                }
            }
            if (Reproducesong != null)
            {
                if (!AlbumSearchArtistListbox.SelectedItem.Equals("Cancion"))
                {
                    namesong = Reproducesong(this, new SelectSongEventArgs() { Selectedsong = Convert.ToString(AlbumSearchArtistListbox.SelectedItem) });
                    pasua = false;
                }

            }

            if (Recivingsong != null)
            {
                if (!AlbumSearchArtistListbox.SelectedItem.Equals("Cancion"))
                {
                    variablecancion = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(AlbumSearchArtistListbox.SelectedItem) });
                    songbeingreproduced = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(AlbumSearchArtistListbox.SelectedItem) });
                    cancionesdelusuario.Add(variablecancion);
                    pasua = false;
                }

            }
            if (Recivingvideo != null)
            {
                if (!AlbumSearchArtistListbox.SelectedItem.Equals("Video"))
                {
                    variablevideo = Recivingvideo(this, new ReturnVideoEventArgs() { Verifyvideoinvideoofuser = Convert.ToString(AlbumSearchArtistListbox.SelectedItem) });
                    videosdelusuario.Add(variablevideo);
                    pasua = false;
                }
            }

        }
        private void FollowArtist_Click(object sender, EventArgs e)
        {
            string selectedartist = Convert.ToString(SearchArtistListBox.SelectedItem);
            Artist artist = getArtist(this, new GetArtistEventArgs() { ArtistName = selectedartist });
            User logeduser = Userrequest(this, new LoginEventArgs() { UsernameText = nameuser });
            logeduser.FollowArtist(artist);
            MessageBox.Show("Siguiendo Artista");
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            MultifiltrolistBox1.ResetText();
            MultifiltrolistBox1.Items.Clear();
            SubFiltersPanel.Visible = false;
            FiltroPanel.Visible = false;
            if (MultifiltroPanel.Visible)
            {
                MultifiltroPanel.Visible = false;
            }
            else
            {
                MultifiltroPanel.Visible = true;
            }
        }

        private void MultiFiltrotextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void MultifiltrolistBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DeleteVideoButton_Click(object sender, EventArgs e)
        {
            string playlist_seleccionada = Convert.ToString(MyVideoListBox.SelectedItem);
            string songseleccionda = Convert.ToString(MisVideoMyPlaylist.SelectedItem);

            foreach (PlaylistVideo videosinplaylistsong in OnReciveUsernamePlaylistVideo())
            {
                if (videosinplaylistsong.GetPlaylistName() == playlist_seleccionada)
                {
                    try
                    {
                        foreach (Video video in videosinplaylistsong.GetPlaylistAllVideos())
                        {
                            if (video.VideoName == songseleccionda)
                            {
                                videosinplaylistsong.RemoveSong(video);
                                MisVideoMyPlaylist.Items.Remove(songseleccionda);
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void BuscarMultiplesFiltroButton_Click(object sender, EventArgs e)
        {
            MultifiltrolistBox1.Items.Clear();
            string word = MultiFiltrotextBox1.Text;
            if (Recivesongmultiplecriteria != null)
            {
                List<Song> songsmultiplefilters = Recivesongmultiplecriteria(this, new SendingtextMultipleFiltersEventArgs() { TexttoMultipleFilters = word });
                foreach (Song s in songsmultiplefilters)
                {
                    MultifiltrolistBox1.Items.Add(s.ToString());
                }
            }
            if (Recivingvideomultiplecriteria != null)
            {
                List<Video> videosmultiplefilters = Recivingvideomultiplecriteria(this, new SendingtextMultipleFiltersEventArgs() { TexttoMultipleFilters = word });
                foreach (Video s in videosmultiplefilters)
                {
                    MultifiltrolistBox1.Items.Add(s.ToString());
                }


            }



        }

        private void InfoFButton_Click(object sender, EventArgs e)
        {
            if (SearchMediapanellistBox.SelectedItem != null)
            {
                string mediaName = SearchMediapanellistBox.SelectedItem.ToString();
                infoSearch(mediaName);
            }
        }

        private void infoSearch(string mName)
        {
            string mediaName = mName;
            if (mediaName == "-----Videos encontrados-----" || mediaName == "-----Canciones encontradas-----")
            {

            }
            else
            {

                if (Recivingsong != null)
                {
                    Song song = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = mediaName });
                    if (song == null)
                    {

                    }
                    else
                    {
                        MessageBox.Show("\n Nombre cancion: " + song.Namesong + "\n Genero: " + song.Genre + "\n Compositor: " + song.Composer + "\n Discografia: " + song.Discography + "\n Estudio: " + song.Studio + "\n Año publicacion: " + song.Publicationyear + "\n Lirica: " + song.Lyrics + "\n Duracion: " + song.Duration + "\n Categoria: " + song.Category + "\n Calificacion: " + song.Qualification + "\n Reproducciones: " + song.Reproduction + "\nTamaño: " + song.Byts);
                    }
                }
                if (Recivingvideo != null)
                {
                    Video video = Recivingvideo(this, new ReturnVideoEventArgs() { Verifyvideoinvideoofuser = mediaName });
                    if (video == null)
                    {

                    }
                    else
                    {
                        MessageBox.Show("\n Nombre cancion: " + video.VideoName + "\n Genero: " + video.Genre + "\n Categoria: " + video.Category + "\n Actor: " + video.Actor + "\n Director: " + video.Director + "\n Estudio: " + video.Studio + "\n Fecha de subida: " + video.UploadDate + "\n Descripcion: " + video.Description + "\n Duracion: " + video.Duration + "\n Calificacion: " + video.Qualification + "\n Reproducciones: " + video.Reproduction + "\nTamaño: " + video.Byts + "\n Resolucion: " + video.Resolution);
                    }
                }
            }
        }

        private void InfoMFbutton_Click(object sender, EventArgs e)
        {
            if (MultifiltrolistBox1.SelectedItem != null)
            {
                string mediaName = MultifiltrolistBox1.SelectedItem.ToString();
                infoSearch(mediaName);
            }

        }
        //------------------------------------------------------ADMIN------------------------------------------------------------------------------//
        private void AdminSearchAristTextbox_TextChanged(object sender, EventArgs e)
        {
            string artist = AdminSearchAristTextbox.Text;
            List<string> results = new List<string>();
            if (artist.Length >= 2)
            {
                CleanSearch();
                Noresult();
                if (Artistinfo != null)
                {
                    Artistinfo(this, new ArtistInfoEventArgs() { ArtistText = artist });
                }

            }

        }
        public void UpdateResultsArtistAdmin(List<string> results)
        {
            SerializeData();
            if (results.Count > 0)
            {
                foreach (string result in results)
                {
                    if (resultCounter <= 50)
                    {
                        if (AdminSearchAristlistBox1.Items.Count > 0 && AdminSearchAristlistBox1.Items[0].Equals("No results for search criteria"))
                        {
                            AdminSearchAristlistBox1.Items.Add(result);
                            AdminSearchAristlistBox1.Items.RemoveAt(0);

                        }
                        else
                            AdminSearchAristlistBox1.Items.Add(result);
                        resultCounter++;
                    }
                }
            }
        }


        private void InfoAristisListbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ArtistInfoButton_Click(object sender, EventArgs e)
        {

            string artistname = AdminSearchAristlistBox1.SelectedItem.ToString();
            InfoAristisListbox.Visible = true;
            if (artistname == "-----Artistas encontrados-----" || artistname == "No results for search criteria")
            {

            }
            else
            {
                InfoAristisListbox.Items.Clear();
                InfoAristisListbox.Visible = true;
                if (Userrequest != null)
                {
                    Artist artis = getArtist(this, new GetArtistEventArgs() { ArtistName = artistname });
                    InfoAristisListbox.Items.Add("Infomracion Artista");
                    InfoAristisListbox.Items.Add("");
                    InfoAristisListbox.Items.Add("Nombre: " + artis.Name);
                    InfoAristisListbox.Items.Add("Artista: " + artis.Artisttype);
                    InfoAristisListbox.Items.Add("Edad: " + artis.Age);
                    InfoAristisListbox.Items.Add("Genero: " + artis.Gender);

                }
            }

        }

        private void SearchAdminArtistButton_Click(object sender, EventArgs e)
        {
            AdminSearchAristlistBox1.Items.Clear();
            AdminSearchAristTextbox.ResetText();
            InfoAristisListbox.Visible = false;
            InfoAristisListbox.Items.Clear();
            AdminUserInfoListBox.Visible = false;
            AdminUserMainPanel.Visible = false;
            if (AdminArtistSearchPanl.Visible)
            {
                AdminArtistSearchPanl.Visible = false;
            }
            else
            {
                AdminArtistSearchPanl.Visible = true;
            }
        }

        private void AdminArtistSearchPanl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SearchAdminButton_Click(object sender, EventArgs e)
        {
            AdminSearchMainPanel.Visible = true;
            SubAdminUploadContent.Visible = false;
            UploadAdminMainPanel.Visible = false;
            if (SubAdminSearchPanel.Visible)
            {
                SubAdminSearchPanel.Visible = false;
            }
            else
            {
                SubAdminSearchPanel.Visible = true;
            }
        }

        private void UploadContenButton_Click(object sender, EventArgs e)
        {
            SubAdminSearchPanel.Visible = false;
            AdminSearchMainPanel.Visible = false;
            UploadAdminMainPanel.Visible = true;
            if (SubAdminUploadContent.Visible)
            {
                SubAdminUploadContent.Visible = false;
            }
            else
            {
                SubAdminUploadContent.Visible = true;
            }

        }

        private void InfoUserButton_Click(object sender, EventArgs e)
        {
            string usr;
            AdminUserInfoListBox.Items.Clear();
            usr = AdminSearchUserlistBox.SelectedItem.ToString();
            if (usr == "-----Usuarios encontrados-----" || usr == "No results for search criteria")
            {

            }
            else
            {
                AdminUserInfoListBox.Visible = true;
                if (Userrequest != null)
                {
                    User user = Userrequest(this, new LoginEventArgs() { UsernameText = usr });
                    AdminUserInfoListBox.Items.Add("Infomracion usuario");
                    AdminUserInfoListBox.Items.Add("");
                    AdminUserInfoListBox.Items.Add("Nombre: " + user.Name + "" + user.Lastname);
                    AdminUserInfoListBox.Items.Add("Usuario: " + user.Username);
                    AdminUserInfoListBox.Items.Add("Mail: " + user.Mail);
                    AdminUserInfoListBox.Items.Add("Edad: " + user.Edad);
                    AdminUserInfoListBox.Items.Add("Genero: " + user.Genero);
                    AdminUserInfoListBox.Items.Add("Privacidad: " + (user.Privacidad));
                    AdminUserInfoListBox.Items.Add("Tipo de cuenta: " + user.Tipodeusuario);
                    AdminUserInfoListBox.Items.Add("Artista: " + user.Artist);

                }
            }

        }

        private void SearchAdminUserbutton_Click(object sender, EventArgs e)
        {
            AdminSearchUserlistBox.Items.Clear();
            AdminSearchUserTextBox.Clear();
            AdminUserInfoListBox.Visible = false;
            AdminArtistSearchPanl.Visible = false;
            if (AdminUserMainPanel.Visible)
            {
                AdminUserMainPanel.Visible = false;
                AdminSearchUserlistBox.Items.Clear();
            }
            else
            {
                AdminUserMainPanel.Visible = true;
                AdminSearchUserlistBox.Items.Clear();
            }
        }

        private void AdminSearchUserTextBox_TextChanged(object sender, EventArgs e)
        {
            SerializeData();
            string searchtextuser = AdminSearchUserTextBox.Text;
            List<string> results = new List<string>();

            if (searchtextuser.Length >= 2)
            {
                CleanSearch();
                Noresult();
                if (Searching != null)
                {
                    Searching(this, new SearchUserEventArgs() { SearchText = searchtextuser });
                }

            }


        }
        public void UpdateResultsAdmin(List<string> results)
        {
            if (results.Count > 0)
            {
                foreach (string result in results)
                {
                    if (resultCounter <= 50)
                    {
                        if (AdminSearchUserlistBox.Items.Count > 0 && AdminSearchUserlistBox.Items[0].Equals("No results for search criteria"))
                        {
                            AdminSearchUserlistBox.Items.Add(result);
                            AdminSearchUserlistBox.Items.RemoveAt(0);
                        }
                        else
                        {
                            AdminSearchUserlistBox.Items.Add(result);
                        }
                        resultCounter++;
                    }
                }
            }
        }

        private void AdminUserInfoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ExitAdminButton_Click(object sender, EventArgs e)
        {
            AdminMainPanel.Visible = false;
            UsernameInPutLogin.ResetText();
            PasswordInPutLogin.ResetText();

        }

        private void UploadAdminSongButton_Click(object sender, EventArgs e)
        {
            AdminUpLoadSongPanel.Visible = true;
            AdminSongSexoCombobox.SelectedIndex = 0;
            AdminUploadVideoPanel.Visible = false;
            AdminSongCategoriaTextBox.ResetText();
            AdminSongCompositorTextBox.ResetText();
            AdminSongDiscografiaTextBox.ResetText();
            AdminSongDuracionTextBox.ResetText();
            AdminSongEdadTextBox.ResetText();
            AdminSongEstudioTextBox.ResetText();
            AdminSongGeneroTextBox.ResetText();
            AdminSongLetraTextBox.ResetText();
        }

        private void UploadAdminVideoButton_Click(object sender, EventArgs e)
        {
            AdminUpLoadSongPanel.Visible = false;
            AdminUploadVideoPanel.Visible = true;
            AdminVideoActorTextbox.ResetText();
            AdminVideoCategoriaTextbox.ResetText();
            AdminVideoDescipcionTextbox.ResetText();
            AdminVideoDirectorTextbox.ResetText();
            AdminVideoDuracionTextbox.ResetText();
            textBox1.ResetText();
            AdminVideoEstudioTextbox.ResetText();
            AdminVideoGeneroTextbox.ResetText();
            AdminVideoSexoCombobox.SelectedIndex = 0;
            AdminVideoResolucionCOmbobox.SelectedIndex = 0;
        }

        private void AdminUploadSongButton_Click(object sender, EventArgs e)
        {
            string songCategoria = null;
            string songGender = null;
            string songDiscorafia = null;
            string songLetra = null;
            string songEtudio = null;
            string songCompositor = null;
            string songDuracion = null;
            string sexo = null;
            string age = null;
            string path;
            string songName;
            songCategoria = AdminSongCategoriaTextBox.Text;
            songCompositor = AdminSongCompositorTextBox.Text;
            songDiscorafia = AdminSongDiscografiaTextBox.Text;
            songDuracion = AdminSongDuracionTextBox.Text;
            songDuracion = songDuracion + " Segundos";
            age = AdminSongEdadTextBox.Text;
            songEtudio = AdminSongEstudioTextBox.Text;
            songGender = AdminSongGeneroTextBox.Text;
            songLetra = AdminSongLetraTextBox.Text;
            sexo = AdminSongSexoCombobox.SelectedItem.ToString();
            if (songCategoria == "" || songGender == "" || songDiscorafia == "" || songLetra == "" || songEtudio == "" || songCompositor == "" || songDuracion == "" || sexo == "None" || age == "" || songCompositor.Length < 3)
            {
                MessageBox.Show("Debe Rellenar todos los cambos");
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Selecione la cancion";
                openFileDialog.Filter = "Song file (*.MP3; *.mp3; *.Vorbis; *.Musepack; *.AAC; *.WMA; *.Opus;)|*.MP3; *.mp3 *.Vorbis; *.Musepack; *.AAC; *.WMA; *.Opus;";
                bool exist = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    songName = openFileDialog.SafeFileName;
                    if (verfyedsong != null)
                    {
                        exist = verfyedsong(this, new SongsExistEventsArtgs() { SongName = songName });
                    }
                    if (exist)
                    {
                        MessageBox.Show("Esta cancion ya existe");
                        AdminSongCategoriaTextBox.ResetText();
                        AdminSongCompositorTextBox.ResetText();
                        AdminSongDiscografiaTextBox.ResetText();
                        AdminSongDuracionTextBox.ResetText();
                        AdminSongEdadTextBox.ResetText();
                        AdminSongEstudioTextBox.ResetText();
                        AdminSongGeneroTextBox.ResetText();
                        AdminSongLetraTextBox.ResetText();
                        AdminSongSexoCombobox.SelectedIndex = 0;
                    }
                    else
                    {
                        path = openFileDialog.FileName;
                        var fileInfo = new FileInfo(path);
                        long tamaño = fileInfo.Length;
                        string songBytes = tamaño.ToString() + "bytes";
                        if (Songcaracteristics != null)
                        {
                            try
                            {
                                Songcaracteristics(this, new SendingsongcaracteristicsEventArgs() { Nombrecancion = songName, Genero = songGender, Compositor = songCompositor, Discografia = songDiscorafia, Estudio = songEtudio, Letra = songLetra, Sexo = sexo, Edad = age, Categoria = songCategoria, path = path, byts = songBytes, duracion = songDuracion });
                                MessageBox.Show("Cancion subida con exito");
                                if (Artistifosend != null)
                                {
                                    Artistifosend(this, new SendingArtistInfo() { Usernametext = songCompositor, ArtistText = "Cantante", AgeArtist = age, GenderArtist = sexo });
                                }
                                AdminSongCategoriaTextBox.ResetText();
                                AdminSongCompositorTextBox.ResetText();
                                AdminSongDiscografiaTextBox.ResetText();
                                AdminSongDuracionTextBox.ResetText();
                                AdminSongEdadTextBox.ResetText();
                                AdminSongEstudioTextBox.ResetText();
                                AdminSongGeneroTextBox.ResetText();
                                AdminSongLetraTextBox.ResetText();
                                AdminSongSexoCombobox.SelectedIndex = 0;
                            }
                            catch
                            {
                                MessageBox.Show("Te faltan datos por rellenar en tu cuenta");
                            }
                        }
                    }
                }
                SerializeData();
            }
        }

        private void AdminUploadVideoButton_Click(object sender, EventArgs e)
        {
            string videoCategoria = null;
            string videoGender = null;
            string videoDescripcion = null;
            string videoResolucion = null;
            string videoEtudio = null;
            string videoDuracion = null;
            string actor = null;
            string director = null;
            string sexo = null;
            string age = null;
            string path;
            string videoName;
            videoCategoria = AdminVideoCategoriaTextbox.Text;
            videoGender = AdminVideoGeneroTextbox.Text;
            videoDescripcion = AdminVideoDescipcionTextbox.Text;
            videoEtudio = AdminVideoEstudioTextbox.Text;
            videoDuracion = AdminVideoDuracionTextbox.Text;
            videoDuracion = videoDuracion + " Segundos";
            actor = AdminVideoActorTextbox.Text;
            director = AdminVideoDirectorTextbox.Text;
            sexo = AdminVideoSexoCombobox.SelectedItem.ToString();
            age = textBox1.Text;
            videoResolucion = AdminVideoResolucionCOmbobox.SelectedItem.ToString();
            if (videoCategoria == "" || videoGender == "" || videoDescripcion == "" || videoEtudio == "" || videoDuracion == "" || actor == "" || director == "" || sexo == "None" || age == "")
            {
                MessageBox.Show("Debe Rellenar todos los cambos");
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Selecione elvideo";
                openFileDialog.Filter = "Video file (*.MP4; *.WEBM;*AVI; *.MPG; *.H264;*.MOV;*.WMV;)| *.MP4; *.WEBM;*.AVI; *.MPG; *.H264;*.MOV;*.WMV;";
                bool exist = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    videoName = openFileDialog.SafeFileName;
                    path = openFileDialog.FileName;
                    var fileInfo = new FileInfo(path);
                    long tamaño = fileInfo.Length;
                    string videoBytes = tamaño.ToString() + "bytes";
                    if (verifyVideoExist != null)
                    {
                        exist = verifyVideoExist(this, new VideosExistEventsArtgs() { VideoNameText = videoName });
                    }
                    if (exist)
                    {
                        MessageBox.Show("Esta video ya existe");
                        AdminVideoActorTextbox.ResetText();
                        AdminVideoCategoriaTextbox.ResetText();
                        AdminVideoDescipcionTextbox.ResetText();
                        AdminVideoDirectorTextbox.ResetText();
                        AdminVideoDuracionTextbox.ResetText();
                        textBox1.ResetText();
                        AdminVideoEstudioTextbox.ResetText();
                        AdminVideoGeneroTextbox.ResetText();
                        AdminVideoSexoCombobox.SelectedIndex = 0;
                        AdminVideoResolucionCOmbobox.SelectedIndex = 0;

                    }
                    else
                    {
                        if (Videocaracteristics != null)
                        {
                            Videocaracteristics(this, new SendingvideocaracteristicsEventArgs() { Videoname = videoName, Genero = videoGender, Categoria = videoCategoria, Actor = actor, Director = director, Estudio = videoEtudio, Descripcion = videoDescripcion, Sexo = sexo, Edad = age, Resolution = videoResolucion, path = path, byts = videoBytes, duracion = videoDuracion });
                            MessageBox.Show("Video subido con exito");
                            if (Artistifosend != null)
                            {
                                Artistifosend(this, new SendingArtistInfo() { Usernametext = actor, ArtistText = "Actor", AgeArtist = age, GenderArtist = sexo });
                                Artistifosend(this, new SendingArtistInfo() { Usernametext = director, ArtistText = "Director", AgeArtist = age, GenderArtist = sexo });
                            }
                            AdminVideoActorTextbox.ResetText();
                            AdminVideoCategoriaTextbox.ResetText();
                            AdminVideoDescipcionTextbox.ResetText();
                            AdminVideoDirectorTextbox.ResetText();
                            AdminVideoDuracionTextbox.ResetText();
                            textBox1.ResetText();
                            AdminVideoEstudioTextbox.ResetText();
                            AdminVideoGeneroTextbox.ResetText();
                            AdminVideoSexoCombobox.SelectedIndex = 0;
                            AdminVideoResolucionCOmbobox.SelectedIndex = 0;
                        }
                    }
                }
                SerializeData();
            }


        }

        private void MasEsuchadaListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerializeData();

            if (Reproducevideo != null)
            {
                if (!MasEsuchadaListBox.SelectedItem.Equals("No results for search criteria"))
                {
                    namevideo = Reproducevideo(this, new SelectVideoEventArgs() { Selectedvideo = Convert.ToString(MasEsuchadaListBox.SelectedItem) });
                    pasua = false;
                }
            }
            if (Recivingvideo != null)
            {
                if (!MasEsuchadaListBox.SelectedItem.Equals("No results for search criteria"))
                {
                    variablevideo = Recivingvideo(this, new ReturnVideoEventArgs() { Verifyvideoinvideoofuser = Convert.ToString(MasEsuchadaListBox.SelectedItem) });
                    videosdelusuario.Add(variablevideo);
                    pasua = false;
                }
            }
            if (Reproducesong != null)
            {
                if (!MasEsuchadaListBox.SelectedItem.Equals("No results for search criteria"))
                {
                    namesong = Reproducesong(this, new SelectSongEventArgs() { Selectedsong = Convert.ToString(MasEsuchadaListBox.SelectedItem) });
                    pasua = false;
                }

            }
            if (Recivingsong != null)
            {
                if (MasEsuchadaListBox.SelectedItem.Equals("No results for search criteria"))
                {
                    variablecancion = Recivingsong(this, new ReturnsongEventArgs() { Verifysonginsongofuser = Convert.ToString(MasEsuchadaListBox.SelectedItem) });
                    cancionesdelusuario.Add(variablecancion);
                    pasua = false;
                }
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (songqueuelist != null)
            {
                if (songqueuelist.Count() != 0)
                {
                    if (songqueuelist[0] != null)
                    {
                        namesong = songqueuelist[0].Path;
                        pasua = false;
                    }
                }
            }

            if (videoqueuelist != null)
            {
                if (videoqueuelist.Count() != 0)
                {
                    if (videoqueuelist[0] != null)
                    {
                        namevideo = videoqueuelist[0].Path;
                        pasua = false;
                    }
                }
            }

            actuallogeduser = Userrequest(this, new LoginEventArgs() { UsernameText = nameuser });

            if (songqueuelist != null)
            {
                if (songqueuelist.Count() != 0)
                {
                    historialsong.Add(songqueuelist[0]);
                }
            }
            if (videoqueuelist != null)
            {
                if (videoqueuelist.Count() != 0)
                {
                    historialvideo.Add(videoqueuelist[0]);
                }
            }

            List<Song> updatedsongqueue = actuallogeduser.GetNextSongQueue();
            List<Video> updatedvideoqueue = actuallogeduser.GetNextVideoQueue();

            songqueuelist = updatedsongqueue;
            videoqueuelist = updatedvideoqueue;

            QueueListBox.Items.Clear();

            if (songqueuelist != null)
            {
                if (songqueuelist.Count() != 0) {
                    QueueListBox.Items.Add("---Canciones---");
                    foreach (Song song in songqueuelist)
                    {
                        if (song != null)
                        {
                            QueueListBox.Items.Add(song.Namesong);
                        }
                    }
                }
            }

            if (videoqueuelist != null)
            {
                if (videoqueuelist.Count() != 0)
                {
                    QueueListBox.Items.Add("---Videos---");
                    foreach (Video video in videoqueuelist)
                    {
                        if (video != null)
                        {
                            QueueListBox.Items.Add(video.VideoName);
                        }
                    }
                }
            }

            if (pasua)
            {
                //ReproduccionMainPanel.Visible = false;
                axWindowsMediaPlayer1.Ctlcontrols.play();

            }
            else
            {
                if (namesong != "" && namevideo == "")
                {
                    axWindowsMediaPlayer1.URL = namesong;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    if (Reproduccionesname != null)
                    {
                        Reproduccionesname(this, new ReproduccionesEventArgs() { Nametext = namesong });
                    }
                    pasua = true;
                }
                else if (namesong == "" && namevideo != "")
                {
                    axWindowsMediaPlayer1.URL = namevideo;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    if (Reproduccionesname != null)
                    {
                        Reproduccionesname(this, new ReproduccionesEventArgs() { Nametext = namevideo });

                    }
                    pasua = true;

                }

            }
            
            if (QueueListBox.Items.Count == 0)
            {
                if (emptyqueue)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    namevideo = "";
                    namesong = "";
                }
                emptyqueue = true;
            }

        }

        //agrgar Al queue
        private void LikeButton_Click(object sender, EventArgs e)
        {
            if (songqueuelist != null)
            {
                List<Song> newlist = new List<Song>();
                newlist.Add(variablecancion);
                foreach (Song elemntsong in songqueuelist)
                {
                    newlist.Add(elemntsong);
                }
                songqueuelist = newlist;
                emptyqueue = false;
            }

            if (videoqueuelist != null)
            {
                List<Video> newlistvideo = new List<Video>();
                newlistvideo.Add(variablevideo);
                foreach (Video elemntsong in videoqueuelist)
                {
                    newlistvideo.Add(elemntsong);
                }
                videoqueuelist = newlistvideo;
                emptyqueue = false;
            }
        }
        //Eliminar de playlist

        private void EliminarMediaQueueButton_Click(object sender, EventArgs e)
        {
            if (songqueuelist != null)
            {
                string selectedsong = QueueListBox.SelectedItem.ToString();
                foreach (Song song in songqueuelist)
                {
                    if (song.Namesong == selectedsong)
                    {
                        songqueuelist.Remove(song);
                        break;
                    }
                }
            }

            if (videoqueuelist != null)
            {
                string selectedsong = QueueListBox.SelectedItem.ToString();
                foreach (Video video in videoqueuelist)
                {
                    if (video.VideoName == selectedsong)
                    {
                        videoqueuelist.Remove(video);
                        break;
                    }
                }
            }

            QueueListBox.Items.Clear();

            if (songqueuelist != null)
            {
                if (songqueuelist.Count() != 0)
                {
                    QueueListBox.Items.Add("---Canciones---");
                    foreach (Song song in songqueuelist)
                    {
                        QueueListBox.Items.Add(song.Namesong);
                    }
                }
            }

            if (videoqueuelist != null)
            {
                if (videoqueuelist.Count() != 0)
                {
                    QueueListBox.Items.Add("---Videos---");
                    foreach (Video video in videoqueuelist)
                    {
                        QueueListBox.Items.Add(video.VideoName);
                    }
                }
            }



        }

        private void BackButton_Click(object sender, EventArgs e)
        {

            if (historialsong != null)
            {
                if (historialsong.Count() != 0)
                {
                    namesong = historialsong[0].Path;
                    pasua = false;
                }
            }

            if (historialvideo != null)
            {
                if (historialvideo.Count() != 0)
                {
                    namevideo = historialvideo[0].Path;
                    pasua = false;
                }
            }


            if (pasua)
            {
                //ReproduccionMainPanel.Visible = false;
                axWindowsMediaPlayer1.Ctlcontrols.play();

            }
            else
            {
                if (namesong != "" && namevideo == "")
                {
                    axWindowsMediaPlayer1.URL = namesong;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    if (Reproduccionesname != null)
                    {
                        Reproduccionesname(this, new ReproduccionesEventArgs() { Nametext = namesong });
                    }
                    pasua = true;
                }
                else if (namesong == "" && namevideo != "")
                {
                    axWindowsMediaPlayer1.URL = namevideo;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    if (Reproduccionesname != null)
                    {
                        Reproduccionesname(this, new ReproduccionesEventArgs() { Nametext = namevideo });

                    }
                    pasua = true;
                }
            }

            if (historialsong != null)
            {
                if (historialsong.Count() != 0)
                {
                    historialsong.RemoveAt(0);
                    pasua = false;
                }
            }

            if (historialvideo != null)
            {
                if (historialvideo.Count() != 0)
                {
                    historialvideo.RemoveAt(0);
                    pasua = false;
                }
            }
        }
    }
}