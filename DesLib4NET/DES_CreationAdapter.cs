using System;
using System.Collections.Generic;
using System.Text;

namespace DesLib4NET
{
    public interface DES_CreationAdapter
    {
        void Set2D();
        void Set3D();
        void SetViewWindow(float xmin, float ymin, float xmax, float ymax);
        void AddSuperBaseHeader(DES_SuperBaseHeader header);
        void AddPoint(DES_Point point);
        void AddSegment(DES_Segment segment);
        void AddArc(DES_Arc arc);
        void AddBezier(DES_Bezier bezier);
        void AddNurbs(DES_Nurbs nurbs);
        void AddDimensionInnerRadius();
        void AddDimensionOuterRadius();
        void AddDimensionInnerDiameter();
        void AddDimensionOuterDiameter();
        void AddDimensionDistance(DES_CotationDistance dimension);
        void AddDimensionAngle();
        void AddDimensionArrow();
        void AddText(DES_Text text);
        void AddPose(DES_Pose pose);
        void AddSurface(DES_Surface surface);
        void UpdateQuestionnaire(Dictionary<string, string> questions);
    }
}
