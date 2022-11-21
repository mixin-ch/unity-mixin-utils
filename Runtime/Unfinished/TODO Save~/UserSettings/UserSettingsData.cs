using System;

[Serializable]
// Dont save this to Cloud!
public class UserSettingsData : DataFile
{
    //this variable tells if the user wnats to save his data in the cloud
    //it can be toggled with the play games connect button
    public bool UserAcceptsSocialLogin { get; set; } = true;

    // The section of basic settings
    // =============================

    //the sensitivity and clip of tilting
    public int Sensitivity { get; set; } = 50;
    public int TiltClip { get; set; } = 50;

    //graphic quality in quality settings
    public int Quality { get; set; } = 5;

    //volume
    public int SoundLoudness { get; set; } = 100;
    public int MusicLoudness { get; set; } = 100;

    //show background scenery particles
    public bool BackgroundElements { get; set; } = true;

    //show foreground scenery particles
    public bool ForegroundElements { get; set; } = false;

    //show particles like player dust, water particles, lava particles
    public bool ParticleSystem { get; set; } = true;

    //show animation for lightning 
    public bool Animations { get; set; } = true;

    //show shader / special materials
    public bool Shaders { get; set; } = false;



    // Section of advanced settings
    // ============================

    //show fps display
    public bool ShowTestBanner { get; set; } = true;
    public bool ShowFPS { get; set; } = false;
}
