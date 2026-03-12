import turtle

def draw_koch_segment(t, ln):
    if ln > 6 :
        ln3 = ln // 3 #Во сколько раз увеличелся маштаф
        draw_koch_segment(t, ln3) #Копий стало 4 (кол-во рекурсивных вызовов)
        t.left(60)
        draw_koch_segment(t, ln3)
        t.right(120)
        draw_koch_segment(t, ln3)
        t.left(60)
        draw_koch_segment(t, ln3)
    else:
        t.fd(ln)
        t.left(60)
        t.fd(ln)
        t.right(120)
        t.fd(ln)
        t.left(60)
        t.fd(ln)
    

# Настройка экрана
t = turtle.Turtle()

t.ht()
t.speed(50)

draw_koch_segment(t, 30)

turtle.done()

# N = r^D

# N — количество копий
# r — во сколько раз увеличили масштаб
# D — размерность

# D = log(4) / log(3) = 1.2618 