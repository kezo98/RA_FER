import pygame
import OpenGL
from pygame.locals import *
from OpenGL.GL import *
from OpenGL.GLU import *
import pywavefront
from pywavefront import visualization
import numpy as np

HEIGHT = 1000
WIDTH = 1000
dt = 0.01

IND = 0
IND_MAX = 0

spline_points = np.array([[0, 0, 0],
                          [0, 10, 5],
                          [10, 10, 10],
                          [10, 0, 15],
                          [0, 0, 20],
                          [0, 10, 25],
                          [10, 10, 30],
                          [10, 0, 35],
                          [0, 0, 40],
                          [0, 10, 45],
                          [10, 10, 50],
                          [10, 0, 55]])


def Ri(points, i):
    return np.array([points[i - 1],
                     points[i],
                     points[i + 1],
                     points[i + 2]])


def point(t, i):
    T = np.array([t ** 3, t ** 2, t, 1])
    B = np.array([[-1, 3, -3, 1],
                  [3, -6, 3, 0],
                  [-3, 0, 3, 0],
                  [1, 4, 1, 0]])
    R = Ri(spline_points, i)
    return 1 / 6 * T @ B @ R  # 1/6 * T * B * R


def vec(t, i):
    T = np.array([t ** 2, t, 1])
    B = np.array([[-1, 3, -3, 1],
                  [2, -4, 2, 0],
                  [-1, 0, 1, 0]])
    R = Ri(spline_points, i)

    return 1 / 2 * T @ B @ R


def get_rotate_axis(s, e):
    return np.cross(s, e)


def get_angle(s, e):
    return np.degrees(np.arccos(np.dot(s, e) / (np.linalg.norm(s) * np.linalg.norm(e))))


def get_points():
    all = []
    for i in range(1, len(spline_points) - 3 + 1):
        for t in np.arange(0.0, 1.0 + dt, dt):
            p = point(t, i)
            all.append(p)

    return all


def get_tangents():
    all = []
    for i in range(1, len(spline_points) - 3 + 1):
        for t in np.arange(0.0, 1.0 + dt, dt):
            p = vec(t, i)
            all.append(p)
    return all


def draw_points():
    glPushMatrix()
    glBegin(GL_LINE_STRIP)
    for i in range(1, len(spline_points) - 3 + 1):
        for t in np.arange(0.0, 1.0 + dt, dt):
            p = point(t, i)
            glVertex3f(p[0], p[1], p[2])
    glEnd()
    glPopMatrix()


def scale_translate_values(obj, scaled_size=5):
    obj_box = (obj.vertices[0], obj.vertices[0])
    for vertex in obj.vertices:
        min_v = [min(obj_box[0][i], vertex[i]) for i in range(3)]
        max_v = [max(obj_box[1][i], vertex[i]) for i in range(3)]
        obj_box = (min_v, max_v)

    obj_size = [obj_box[1][i] - obj_box[0][i] for i in range(3)]
    scale_value = [scaled_size / max(obj_size) for i in range(3)]
    translate_value = [-(obj_box[1][i] + obj_box[0][i]) / 2 for i in range(3)]

    return scale_value, translate_value


def draw_tangent(point, tang):
    glPushMatrix()
    glBegin(GL_LINES)
    glVertex3f(point[0], point[1], point[2])
    glVertex3f(point[0] + tang[0]*3, point[1] + tang[1]*3, point[2] + tang[2]*3)
    glEnd()
    glPopMatrix()


def draw_object(path="bird.obj", scale_bool=True, translate_bool=False, scaled_size=5):
    obj = pywavefront.Wavefront(path, collect_faces=True)
    scale_value, translate_value = scale_translate_values(obj, scaled_size)

    glPushMatrix()
    if scale_bool:
        glScalef(*scale_value)
    if translate_bool:
        glTranslatef(*translate_value)
    draw_method_1(obj)
    glPopMatrix()


def draw_method_1(obj):
    for mesh in obj.mesh_list:
        glColor3f(np.random.random(1)[0], np.random.random(1)[0], np.random.random(1)[0])
        glBegin(GL_TRIANGLES)
        for face in mesh.faces:
            for vertex_i in face:
                glVertex3f(*obj.vertices[vertex_i])
        glEnd()


def draw_method_2(obj):
    visualization.draw(obj)


def main():
    global IND
    global IND_MAX
    all_points = get_points()

    all_tangents = get_tangents()

    IND_MAX = len(all_points) - 1
    pygame.init()
    display = (HEIGHT, WIDTH)
    screen = pygame.display.set_mode(display, DOUBLEBUF | OPENGL)

    # glTranslatef(all_points[0][0], all_points[0][1], all_points[0][2])
    # pygame.draw.lines(display, "red", closed=False, points=get_points())
    while True:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()
                quit()
        s = np.array([0, 0, 1])
        e = all_tangents[IND]

        axis = get_rotate_axis(s, e)
        rotate_angle = get_angle(s, e)

        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT)
        glLoadIdentity()
        gluPerspective(45, (display[0] / display[1]), 1, 600)
        gluLookAt(-3, -3, -20,
                  0, 0, 0,
                  0, 1, 0)
        glColor3f(255, 255, 255)
        draw_points()
        glColor3f(255, 0, 255)
        draw_tangent(all_points[IND], e)

        glPolygonMode(GL_FRONT_AND_BACK, GL_LINE)
        glPolygonMode(GL_FRONT_AND_BACK, GL_FILL)
        glTranslatef(all_points[IND][0], all_points[IND][1], all_points[IND][2])

        glRotatef(rotate_angle, axis[0], axis[1], axis[2])
        glPolygonMode(GL_FRONT_AND_BACK, GL_LINE)
        glPolygonMode(GL_FRONT_AND_BACK, GL_FILL)
        draw_object()

        IND = IND + 1
        if IND == IND_MAX:
            IND = 0

        pygame.display.flip()
        pygame.time.wait(10)


if __name__ == '__main__':
    main()
