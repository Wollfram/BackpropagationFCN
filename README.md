# BackpropagationFCN
Програма позволяет построть полносвязную нейронною сеть и демонстрирует процес обучения методом обратного распространения ошибки для  решения задачи "апроксимации" кривой Безье, при этом на вход подаються четыре точки, которые задают кривую, а выход сети (Controls) -  указаное количество значений кутов поворота прямой Безье на отрезках этой кривой (кривая разбиваеться равномерно)

## ![Ссылка](https://github.com/Wollfram/BackpropagationFCN/blob/master/README%20!!!/Kursova_robotaFinal.pptx) на презентацию 


## Созданые класы сети
- Matrix - для выполнения расчётов с матрицами  
- INeuron - интерфейс функции активации, реализовал для тангенциальной (f - прямая, f1 - ее производная)  
- ILayer - интерфейс представляет шар НС, сохраняет веса и функцию активации  
- IAccuracyCalculator - для вычесления точности работы НС  
- INeuroNet - интерфейс сети, реализуеться в класе NeuroNetFullyConnected  
![](https://github.com/Wollfram/BackpropagationFCN/blob/master/README%20!!!/classes.png)  

## Интерфейс програмы
![](https://github.com/Wollfram/BackpropagationFCN/blob/master/README%20!!!/10-10.png)  

## точки кривой - входы сети
![](https://github.com/Wollfram/BackpropagationFCN/blob/master/README%20!!!/Screenshot_2%20(2).png)  
