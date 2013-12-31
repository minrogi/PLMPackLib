#region Using directives
using System;
using System.Windows.Forms;
using System.Drawing;

using Pic.DAL.LibraryLoader;
#endregion

public class ListBoxLibraries : ListBox
{
    public ListBoxLibraries()
	{
        DrawMode = DrawMode.OwnerDrawFixed;
        ItemHeight = 152;
	}

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;

        e.DrawBackground();
        e.DrawFocusRectangle();

        if (DesignMode)
        {
            e.Graphics.DrawRectangle(Pens.Red, 1, e.Bounds.Y + 1, IMAGE_WIDTH, IMAGE_WIDTH);
            var textRect = e.Bounds;
            textRect.X += IMAGE_WIDTH + 1;
            textRect.Width -= IMAGE_WIDTH + 1;
            string itemText = "AddressListBox";
            TextRenderer.DrawText(e.Graphics, itemText, e.Font, textRect, e.ForeColor, flags);
            e.DrawFocusRectangle();
         }
        else if (e.Index >= 0)
        {
            Library lib = Items[e.Index] as Library;
            if (null == lib) return;

            e.Graphics.DrawImage(lib.Thumbnail, 0, e.Bounds.Y + 1);

            var textRect = e.Bounds;
            textRect.X += IMAGE_WIDTH + 1;
            textRect.Width -= IMAGE_WIDTH + 1;

            Font fontBold = new System.Drawing.Font(e.Font, FontStyle.Bold);

            Rectangle itemRect0 = new Rectangle(textRect.X, textRect.Y, 100, 20);
            TextRenderer.DrawText(e.Graphics, "Name         ", e.Font, itemRect0, e.ForeColor, flags); itemRect0.Y += itemRect0.Height;
            TextRenderer.DrawText(e.Graphics, "Description  ", e.Font, itemRect0, e.ForeColor, flags); itemRect0.Y += itemRect0.Height;
            TextRenderer.DrawText(e.Graphics, "Author       ", e.Font, itemRect0, e.ForeColor, flags); itemRect0.Y += itemRect0.Height;
            TextRenderer.DrawText(e.Graphics, "Date created ", e.Font, itemRect0, e.ForeColor, flags); itemRect0.Y += itemRect0.Height;

            Rectangle itemRect1 = new Rectangle(textRect.X + 100, textRect.Y, 10, 20);
            TextRenderer.DrawText(e.Graphics, ":", e.Font, itemRect1, e.ForeColor, flags); itemRect1.Y += itemRect1.Height;
            TextRenderer.DrawText(e.Graphics, ":", e.Font, itemRect1, e.ForeColor, flags); itemRect1.Y += itemRect1.Height;
            TextRenderer.DrawText(e.Graphics, ":", e.Font, itemRect1, e.ForeColor, flags); itemRect1.Y += itemRect1.Height;
            TextRenderer.DrawText(e.Graphics, ":", e.Font, itemRect1, e.ForeColor, flags); itemRect1.Y += itemRect1.Height;

            Rectangle itemRect2 = new Rectangle(textRect.X + 110, textRect.Y, textRect.Width - 110, 20);
            TextRenderer.DrawText(e.Graphics, lib.Name, fontBold, itemRect2, e.ForeColor, flags); itemRect2.Y += itemRect2.Height;
            TextRenderer.DrawText(e.Graphics, lib.Description, e.Font, itemRect2, e.ForeColor, flags); itemRect2.Y += itemRect2.Height;
            TextRenderer.DrawText(e.Graphics, lib.Author, e.Font, itemRect2, e.ForeColor, flags); itemRect2.Y += itemRect2.Height;
            TextRenderer.DrawText(e.Graphics, lib.DateCreated.ToShortDateString(), e.Font, itemRect2, e.ForeColor, flags); itemRect2.Y += itemRect2.Height;
       }
    }

    #region Data members
    private static int IMAGE_WIDTH = 150;
    #endregion
}
