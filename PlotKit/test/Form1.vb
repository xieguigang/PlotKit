﻿Imports ggplot
Imports Microsoft.VisualBasic.Math.DataFrame
Imports PlotKit
Imports Microsoft.VisualBasic.Math.VBMath
Imports Microsoft.VisualBasic.Math.Distributions
Imports Microsoft.VisualBasic.Math
Imports Microsoft.VisualBasic.Math.LinearAlgebra
Imports Microsoft.VisualBasic.Drawing

Public Class Form1

    Dim WithEvents view As New PlotView

    Shared Sub New()
        Call SkiaDriver.Register()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Controls.Add(view)
        view.Dock = DockStyle.Fill

        Dim x As Double() = seq(0, 50, by:=0.05).ToArray
        Dim y1 = Gaussian.Gaussian(x, 12000, 15, 1)
        Dim y2 = Gaussian.Gaussian(x, 300, 27, 2)
        Dim y3 = Gaussian.Gaussian(x, 3600, 35, 4)
        Dim noise = Vector.rand(30, 50, x.Length).ToArray

        Dim test As DataFrame = New DataFrame() _
            .add("x", x) _
            .add("y", SIMD.Add.f64_op_add_f64(SIMD.Add.f64_op_add_f64(SIMD.Add.f64_op_add_f64(y1, y2), y3), noise))
        Dim plot As ggplot.ggplot = ggplotFunction.ggplot(test, mapping:=aes("x", "y"))

        plot += geom_point(size:=12)

        view.ggplot = plot
        test.rownames = x.Select(Function(xi, i) CStr(i + 1)).ToArray

        Call test.WriteCsv("./test_signal.csv")
    End Sub
End Class
