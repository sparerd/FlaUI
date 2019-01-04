using FlaUI.UIA3;
using NUnit.Framework;
using System.Diagnostics;

namespace FlaUI.Core.UITests
{
    [TestFixture]
    public class NotepadTests
    {
        private Application _launchedApp;
        private Application _attachedApp;

        [TearDown]
        public void TearDown()
        {
            _launchedApp?.Close();
            _launchedApp?.Dispose();

            _attachedApp?.Close();
            _attachedApp?.Dispose();
        }

        [Test]
        public void NotepadLaunchTest()
        {
            _launchedApp = Application.Launch("notepad.exe");

            using (var automation = new UIA3Automation())
            {
                var window = _launchedApp.GetMainWindow(automation);
                Assert.That(window, Is.Not.Null);
                Assert.That(window.Title, Is.Not.Null);
            }
        }

        [Test]
        public void NotepadAttachByNameTest()
        {
            _launchedApp = Application.Launch("notepad.exe");
            _attachedApp = Application.Attach("notepad.exe");

            using (var automation = new UIA3Automation())
            {
                var window = _attachedApp.GetMainWindow(automation);
                Assert.That(window, Is.Not.Null);
                Assert.That(window.Title, Is.Not.Null);
            }
        }

        [Test]
        public void NotepadAttachByProcessIdTest()
        {
            _launchedApp = Application.Launch("notepad.exe");
            _attachedApp = Application.Attach(_launchedApp.ProcessId);

            using (var automation = new UIA3Automation())
            {
                var window = _attachedApp.GetMainWindow(automation);
                Assert.That(window, Is.Not.Null);
                Assert.That(window.Title, Is.Not.Null);
            }
        }

        [TestCase(@"C:\WINDOWS\system32\notepad.exe")]
        [TestCase("notepad.exe")]
        public void NotepadAttachOrLauchIdTest(string name)
        {
            _launchedApp = Application.Launch("notepad.exe");
            _attachedApp = Application.AttachOrLaunch(new ProcessStartInfo(name));

            using (var automation = new UIA3Automation())
            {
                var window = _attachedApp.GetMainWindow(automation);
                Assert.That(window, Is.Not.Null);
                Assert.That(window.Title, Is.Not.Null);
            }
        }

        [Test]
        public void NotepadAttachWithAbsoluteExePath()
        {
            _launchedApp = Application.Launch("notepad.exe");
            _attachedApp = Application.Attach(@"C:\WINDOWS\system32\notepad.exe");

            using (var automation = new UIA3Automation())
            {
                var window = _attachedApp.GetMainWindow(automation);
                Assert.That(window, Is.Not.Null);
                Assert.That(window.Title, Is.Not.Null);
            }
        }
    }
}
