'
' Simulation of a Wireframe Cube using GDI+
' Developed by leonelmachava <leonelmachava@gmail.com>
' http://codentronix.com
'
' Copyright (c) 2011 Leonel Machava
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy of this 
' software and associated documentation files (the "Software"), to deal in the Software 
' without restriction, including without limitation the rights to use, copy, modify, 
' merge, publish, distribute, sublicense, and/or sell copies of the Software, and to 
' permit persons to whom the Software is furnished to do so, subject to the following 
' conditions:
' 
' The above copyright notice and this permission notice shall be included in all copies 
' or substantial portions of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
' INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR 
' PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
' FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
' OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'
Imports System.Drawing.Graphics
Imports System.Drawing.Pen
Imports System.Drawing.Color
Imports System.Drawing.Brush
Imports System.Drawing.Point

Public Class Main
    Protected m_pen As Pen
    Protected m_timer As Timer
    Protected m_vertices(8) As Point3D
    Protected m_faces(6, 4) As Integer
    Protected m_angle As Integer

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Create a GDI+ Pen. This will be used to draw lines.
        m_pen = New Pen(Color.Red)

        InitCube()

        ' Create the timer.
        m_timer = New Timer()

        ' Set the timer interval to 33 milliseconds. This will give us 1000/34 ~ 30 frames per second.
        m_timer.Interval = 33

        ' Set the callback for the timer.
        AddHandler m_timer.Tick, AddressOf AnimationLoop

        ' Start the timer.
        m_timer.Start()
    End Sub

    Private Sub InitCube()
        ' Create an array with 8 points.
        m_vertices = New Point3D() {
                     New Point3D(-1, 1, -1),
                     New Point3D(1, 1, -1),
                     New Point3D(1, -1, -1),
                     New Point3D(-1, -1, -1),
                     New Point3D(-1, 1, 1),
                     New Point3D(1, 1, 1),
                     New Point3D(1, -1, 1),
                     New Point3D(-1, -1, 1)}

        ' Create an array representing the 6 faces of a cube. Each face is composed by indices to the vertex array
        ' above.
        m_faces = New Integer(,) {{0, 1, 2, 3}, {1, 5, 6, 2}, {5, 4, 7, 6}, {4, 0, 3, 7}, {0, 4, 5, 1}, {3, 2, 6, 7}}
    End Sub

    Private Sub AnimationLoop()
        ' Forces the Paint event to be called.
        Me.Invalidate()

        ' Update the variable after each frame.
        m_angle += 1
    End Sub

    Private Sub Main_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim t(8) As Point3D
        Dim f(4) As Integer
        Dim v As Point3D

        ' Clear the window
        e.Graphics.Clear(Color.LightBlue)

        ' Transform all the points and store them on the "t"- array.
        For i = 0 To 7
            v = m_vertices(i)
            t(i) = v.RotateX(m_angle).RotateY(m_angle).RotateZ(Me.m_angle)
            t(i) = t(i).Project(Me.ClientSize.Width, Me.ClientSize.Height, 256, 4)
        Next

        ' Draw the wireframe cube. Uses the "m_faces" array to find the vertices that compose each face.
        For i = 0 To 5
            e.Graphics.DrawLine(m_pen, CInt(t(m_faces(i, 0)).X), CInt(t(m_faces(i, 0)).Y), CInt(t(m_faces(i, 1)).X), CInt(t(m_faces(i, 1)).Y))
            e.Graphics.DrawLine(m_pen, CInt(t(m_faces(i, 1)).X), CInt(t(m_faces(i, 1)).Y), CInt(t(m_faces(i, 2)).X), CInt(t(m_faces(i, 2)).Y))
            e.Graphics.DrawLine(m_pen, CInt(t(m_faces(i, 2)).X), CInt(t(m_faces(i, 2)).Y), CInt(t(m_faces(i, 3)).X), CInt(t(m_faces(i, 3)).Y))
            e.Graphics.DrawLine(m_pen, CInt(t(m_faces(i, 3)).X), CInt(t(m_faces(i, 3)).Y), CInt(t(m_faces(i, 0)).X), CInt(t(m_faces(i, 0)).Y))
        Next
    End Sub
End Class