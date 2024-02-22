<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loadingPage.aspx.cs" Inherits="loadingPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .loading {
          opacity: 0;
          transition: opacity 2s ease-in-out;
          position: absolute;
          top: 50%;
          left: 50%;
          transform: translate(-50%, -50%);
        }

        .loading.visible {
          opacity: 1;
        }
    </style>
    <script>
        setTimeout(function () {
            document.querySelector('.loading').classList.add('visible');
        }, 500); // Delay for 0.5 seconds
    </script>
</head>
<body style="background: rgb(241, 242, 243);">
    <form id="form1" runat="server">
    <div class="loading" >
        <div style="align-content:center;">
            <img src="images/Seiha-Eagle-Philippine-Colorways.png"/ style="height: 200px; width: 200px;"/>
        </div>
        <img src="images/Ellipsis-1s-200px.svg" alt="images/Seiha-Eagle-Philippine-Colorways.png"/>
    </div>
    </form>
</body>
</html>
