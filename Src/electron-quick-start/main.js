const {app, BrowserWindow, Menu} = require('electron');
const path = require('path');

let mainWindow;

function createWindow () 
{
  mainWindow = new BrowserWindow(
  {
    width:  800,
    height: 650,
    minHeight:  650,
    minWidth:   450,
    
    titleBarStyle: "hidden", 
    frame: false,
    icon: __dirname + '/Graphics/Icon.png',

    webPreferences: 
    {
      enableRemoteModule: true,
      nodeIntegration: false,
      preload: path.join(__dirname, 'preload.js')
    }
  })

  mainWindow.loadFile('index.html');

  mainWindow.webContents.openDevTools();
}

app.whenReady().then(() => 
{
  createWindow();
  
  app.on('activate', function () 
  {
    if (BrowserWindow.getAllWindows().length === 0) createWindow();
  })
})

app.on('window-all-closed', function () 
{
  if (process.platform !== 'darwin') app.quit();
})
