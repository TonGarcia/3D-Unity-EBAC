# Damage System

## Checkers

GameObject:
1. It needs to have a collider in the same GameObject (same as MeshRenderer);
2. The GameObject with the collider needs to have a tag that is "Enemy";
3. It needs to have a script that inherits from MonoBehaviour;
4. It needs to have a script that inherits from IDamageable (and implement the methods);

IDamageable:
1. It needs to have a method that receives damage;
2. It needs to have a method that receives knockback;
3. It needs to have a method that receives a GameObject;
4. It needs to have a method that receives a float;
5. It needs to have a method that receives a Vector2;

Boss IDamageable implementation: https://github.com/TonGarcia/3D-Unity-EBAC/commit/2bf5991d22c76fca2a5673dc922f33af09bbbbed#diff-92458b988ee6e0372487945fc2280e0defe6089a7ecd789f9e1e112544c7da93R161

1. Enemy IDamageable implementation: https://github.com/TonGarcia/3D-Unity-EBAC/commit/2bf5991d22c76fca2a5673dc922f33af09bbbbed#diff-1750ad9a314636b5b1ae1d873d3c492b1722f4dc5b2a70986958ecb11d002d21R114
2. Enemy IDamageable against Player implementation: https://github.com/TonGarcia/3D-Unity-EBAC/commit/2bf5991d22c76fca2a5673dc922f33af09bbbbed#diff-1750ad9a314636b5b1ae1d873d3c492b1722f4dc5b2a70986958ecb11d002d21R52