using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    #region enums
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    };
    #endregion

    #region fields
    public FitType fitType;
    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;
    public bool fitX;
    public bool fitY;
    #endregion

    #region layoutgroup methods
    public override void CalculateLayoutInputVertical()
    {
        //throw new System.NotImplementedException();
    }

    public override void SetLayoutHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if(fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            fitX = true;
            fitY = true;

            float sqrRt = Mathf.Sqrt(transform.childCount); // used to determine row and column count
            rows = Mathf.CeilToInt(sqrRt);
            columns = Mathf.CeilToInt(sqrRt);
        }

        // set row and column count so that they fill up a single column
        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }
        // set row and column count so that they fill up a single row
        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = ((float) parentWidth / (float) columns) - (((float) spacing.x / (float) columns) * 4f) - ((float)padding.left / (float) columns) - ((float)padding.right / (float)columns);
        float cellHeight = ((float) parentHeight / (float) rows) - (((float) spacing.y / (float) rows) * 4f) - ((float)padding.bottom / (float) rows) - ((float)padding.top / (float)rows);

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];
            
            var xPos = ((float) cellSize.x * (float) columnCount) + ((float) spacing.x * (float) columnCount) + (float) padding.left;
            var yPos = ((float) cellSize.y * (float) rowCount) + ((float) spacing.y * (float) rowCount) + (float) padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void SetLayoutVertical()
    {
        //throw new System.NotImplementedException();
    }
    #endregion
}
