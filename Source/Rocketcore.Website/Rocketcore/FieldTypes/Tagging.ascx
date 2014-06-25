<%@ Control Language="C#" AutoEventWireup="true" Inherits="Rocketcore.Tagging.Controls.TaggingControl" %>
<div id="<%=ID%>_TagField">
    <input id="<%= ID %>_Value" type="hidden" class="tagHiddenInputBox" value="<%= Value %>">
    <div>
	    <p>Enter a tag</p>
        <div class="inputWrap">
            <input id="<%= ID %>_TagEntry" class="tagInputBox" />
            <div class="addtagbtn">Add Tag</div>
            <div class="messageTextBox"></div>
            <div class="tagSuggestionBox_wrap">
            <span class="closebtn"></span>
            <div class="tagSuggestionBox" ></div></div>
        </div>
    </div>
    <div>
        <p>Added Tags</p>
        <div id="<%= ID %>_Selected" class="tagsAdded">
                <% foreach (var selectedValue in SelectedValues) { %>
                <span data-id="<%= selectedValue.Key %>" class="addedTagItem"><span class="displayText"><%= selectedValue.Value %></span><a href="#" class="addedTagItemCloseBtn"></a></span>
            <% } %>
        </div>
    </div>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <link rel="stylesheet" type="text/css" href="/Lightcore/FieldTypes/assets/Tagging/css/rocketcore-tags-field.css"/>
    <!-- Required JS file /Lightcore/FieldTypes/assets/Tagging/js/lightcore-tags-field.js is included inside config "LM.Lightcore.Tagging.config" -->
    <script type="text/javascript">
        jQuery(document).ready(function() {
            var taggingFunctionality<%=ID%> = new TagField("#<%=ID%>_TagField", ".tagHiddenInputBox", ".tagsAdded", ".addtagbtn", ".tagInputBox", ".tagSuggestionBox", "<%= Filter %>", ".messageTextBox");
        });
    </script>
</div>