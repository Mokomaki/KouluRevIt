const remote = require('electron').remote;
const sql = require('mysql');

//window controls  
window.CloseWindow = function() 
{
    remote.BrowserWindow.getFocusedWindow().close();
}
window.MinimizeWindow = function() 
{
    remote.BrowserWindow.getFocusedWindow().minimize();
}
window.MaximizeWindow = function() 
{
    if(remote.BrowserWindow.getFocusedWindow().isMaximized())
    {
        remote.BrowserWindow.getFocusedWindow().unmaximize();
    }
    else
    {
        remote.BrowserWindow.getFocusedWindow().maximize();
    }
}

window.AddPost = function(authorID, title, contents, categoryID)
{
    var connection = sql.createConnection(
        {
            host     : "185.53.85.170",
            port     : "3306",
            user     : "risto_h",
            password : "dbpasswordisristo",
            database : "risto_h"
        }
    );

    connection.connect((err)=> {
            if (err) throw err;
            else
            {
                console.log('connected');
            }
        }
    );

    if(authorID!=null&&title!=null&&contents!=null&&categoryID!=null)
    {
        var command = 'INSERT INTO `posts` (`authorID`,`title`,`contents`,`categoryID`) VALUES ("'+ authorID + '","' + title + '",' +"'" + JSON.stringify(contents) + "'" + ',"' + categoryID + '");'
        connection.query(command, function (error, results, fields) 
        {
            if (error) console.log(error.code);
            else 
            {
                console.log(results);
            }
        });
    }

    connection.end(
        function(err) {
            if (err) {
              return console.log('error:' + err.message);
            }
            console.log('connection ended');
        }
    );
}