<!DOCTYPE html>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <head>
        <style>
          .user span
          {
             padding: 0 20px;
          }
          .editor span{
            width:70px;
            display:inline-block;
          }
          .hidden{
            display:none;
          }
        </style>
        <script src="http://code.jquery.com/jquery.min.js"></script>
        <script>
          $(document).ready(function(){
              enableEditor(false);
              $('input[type=checkbox]').change(function(e){
                var checkedUsers = $('input[type=checkbox]');
                var selected = "";
                var diselected = "";
                for(var i = 0; i &lt; checkedUsers.length; i++){
                  if(!$(checkedUsers[i]).is(':checked')){
                  diselected += $(checkedUsers[i]).attr('id') + ';';
                  }else{
                  selected += $(checkedUsers[i]).attr('id') + ';';
                  }
                }
                $('#selectedUsers').val(selected);
                $('#diselectedUsers').val(diselected);
                 if($('#selectedUsers').val() != ''){
                    $('#deletedBtn').removeAttr('disabled','disabled');
                 }else{
                    $('#deletedBtn').attr('disabled','disabled');
                 }
              });
          
              if($('#selectedUsers').val() == ''){
                    $('#deletedBtn').attr('disabled','disabled');
              }

              $('a.paging').click(function(e){
                var url = $(this).attr('href');
                $('form').attr('action', url );
                $('form').submit();
                return false;
              });

              $('#deletedBtn').click(function(){
                var url = $(this).attr('deleteAction');
                $('form').attr('action', url );
                $('form').submit();
              });
              
              $('.saveBtn').click(function(){
                enableEditor(false);
                var editor = $('.editor');
                var url = $('#updateUrl').attr('updateurl');
                var data = {};
                $('.fields input',editor).each(function(){
                   data[$(this).attr('class')] = $(this).val();
                });
                $.ajax({
                  url: url,
                  data: data,
                  success:function(){
                     var id = $('.userid',editor).val();
                     var user = $('li[userid=' + id + ']');
                     
                     $('span', user).each(function(){
                       var cls = '.' + $(this).attr('class');
                       $(this).html($(cls, editor).val());
                     });
                  }
                });
              });
              
              $('.cancelBtn').click(function(){
                  enableEditor(false);
              });
              
              $('.editBtn').click(function(){
                var editor = $('.editor');
                var user_id = $(this).attr('userId');
                var li = $(this).parent();
                enableEditor(true);
                $('.fields input',editor).each(function(){
                   var cls = '.' + $(this).attr('class');
                   $(cls, editor).val($(cls, li).html());
                });
              });
              
              function enableEditor(enable){
                 var editor = $('.editor');
                 if(enable){
                      editor.show();
                 }else{
                     editor.hide();
                 }
              }

          });

        </script>
      </head>
      <body>
        <a href="/Home/Step1" >Add New</a>
        <form action="/Home/Step1" method="POST">
          <xsl:apply-templates/>
          <input type="hidden" name="selectedUsers" id="selectedUsers"></input>
          <input type="hidden" name="diselectedUsers" id="diselectedUsers"></input>
          <input type="hidden" id="updateUrl">
            <xsl:attribute name="updateUrl">
              <xsl:value-of select="page/updateAction" />
            </xsl:attribute>
          </input>
          <input type="button" deleteAction="Home/DeleteRecord" name="deleteBtn" id="deletedBtn" value="Deleted Selected"></input>

          <div class="editor">
            <div class="fields">
              <p class="hidden">
                <input type="input" class="userid"></input>
              </p>
              <p>
                <span>Name:</span>
                <input type="input" class="name"></input>
              </p>
              <p>
                <span>Surname:</span>
                <input type="input" class="surname"></input>
              </p>
              <p>
                <span>Address:</span>
                <input type="input" class="address"></input>
              </p>
              <p>
                <span>Phone:</span>
                <input type="input" class="phone"></input>
              </p>
            </div>
            <input type="button" class="saveBtn" name="save" value="Save"></input>
            <input type="button" class="cancelBtn" name="cancel" value="Cancel"></input>
          </div>
        </form>
      </body>
    </html>
  </xsl:template>


  <xsl:template match="users">
    <ul>
       <xsl:apply-templates select="user"/>
    </ul>
  </xsl:template>

  <xsl:template match="updateAction">
  </xsl:template>
  
  <xsl:template match="user">
    <li class="user">
      <xsl:attribute name="userId">
        <xsl:value-of select="User_Id" />
      </xsl:attribute>
      <input type="checkbox">
        <xsl:if test="Checked='True'">
          <xsl:attribute name="checked">
            <xsl:value-of select="Checked" />
          </xsl:attribute>
        </xsl:if>
        <xsl:attribute name="id">
          <xsl:value-of select="User_Id" />
        </xsl:attribute>
      </input>
      <p class="hidden">
        <span class="userid">
          <xsl:value-of select="User_Id" />
        </span>
      </p>
      <span class="name">
        <xsl:value-of select="Name" />
      </span>
      <span class="surname">
        <xsl:value-of select="Surname" />
      </span>
      <span class="address">
        <xsl:value-of select="Address" />
      </span>
      <span class="phone">
        <xsl:value-of select="Phone" />
      </span>
      <input type="button" name="editBtn" class="editBtn" value="Edit">
        <xsl:attribute name="userId">
          <xsl:value-of select="User_Id" />
        </xsl:attribute>
      </input>
    </li>
  </xsl:template>
  
  <xsl:template match="paging">
    <xsl:if test="showFirst='True'">
      <a class="paging">
        <xsl:attribute name="href">
          <xsl:value-of select="baseUrl" />
          <xsl:value-of select="firstUrl" />
        </xsl:attribute>
        First
      </a>
    </xsl:if>
    <xsl:if test="showPrev='True'">
      <a class="paging">
        <xsl:attribute name="href">
          <xsl:value-of select="baseUrl" />
          <xsl:value-of select="prevUrl" />
        </xsl:attribute>
        Prev
      </a>
    </xsl:if>
    <span><xsl:value-of select="current"/> / <xsl:value-of select="total"/></span>
    <xsl:if test="showNext='True'">
      <a class="paging">
        <xsl:attribute name="href">
          <xsl:value-of select="baseUrl" />
          <xsl:value-of select="nextUrl" />
        </xsl:attribute>
        Next
      </a>
    </xsl:if>
    <xsl:if test="showLast='True'">
      <a class="paging">
        <xsl:attribute name="href">
          <xsl:value-of select="baseUrl" />
          <xsl:value-of select="lastUrl" />
        </xsl:attribute>
        Last
      </a>
    </xsl:if>
  </xsl:template>
  
</xsl:stylesheet>



