using System;
using System.Diagnostics;
using System.Numerics;
using ImGuiNET;
using ClickableTransparentOverlay;

public partial class Program : Overlay
{
    private enum MenuTab
    {
        Patch,
        EncoderDecoder,
        About
    }

    private MenuTab currentTab = MenuTab.Patch;

    public Program(int width, int height) : base(width, height)
    {
    }

    protected override void Render()
    {
        ImGui.Begin("BoncukHook");

        ImGui.SetWindowSize(new Vector2(400, 300), ImGuiCond.Once);

        // Tablar arası seçim menüsü
        if (ImGui.BeginTabBar("MainTabs"))
        {
            if (ImGui.BeginTabItem("Patch"))
            {
                currentTab = MenuTab.Patch;
                RenderPatchTab();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Encoder / Decoder"))
            {
                currentTab = MenuTab.EncoderDecoder;
                RenderEncoderDecoderTab();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("About"))
            {
                currentTab = MenuTab.About;
                RenderAboutTab();
                ImGui.EndTabItem();
            }

            ImGui.EndTabBar();
        }

        ImGui.End();
    }

    private void RenderPatchTab()
    {
        ImGui.Text("Patch seviyesini seçin:");

        if (ImGui.Button("Patch 10 dB"))
        {
            Process.Start("patch_10db.exe");
        }

        if (ImGui.Button("Patch 100 dB"))
        {
            Process.Start("patch_100db.exe");
        }

        if (ImGui.Button("Patch 1000 dB"))
        {
            Process.Start("patch_1000db.exe");
        }

        ImGui.Spacing();

        if (ImGui.Button("Discord'u Kapat (Kill)"))
        {
            foreach (var proc in Process.GetProcessesByName("Discord"))
            {
                try
                {
                    proc.Kill();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Hata oluştu: {e.Message}");
                }
            }
        }
    }

    private void RenderEncoderDecoderTab()
    {
        ImGui.Text("Buraya encoder / decoder ayarları eklenebilir.");
        ImGui.BulletText("Geliştirilebilir alan");
        ImGui.BulletText("Bitrate, stereo, mono vb.");
    }

    private void RenderAboutTab()
    {
        ImGui.Text("BoncukHook by Yusuf & Boncuk");
        ImGui.Separator();
        ImGui.Text("Bu araç Discord ses modülü üzerinde dB patch uygulamak için yapılmıştır.");
        ImGui.Text("Kullanım tamamen eğitim ve test amaçlıdır.");
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("BoncukHook Services Boncuk&Yusuf");
        Program program = new Program(400, 300);
        program.Run();
    }
}
