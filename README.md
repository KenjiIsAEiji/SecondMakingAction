# SecondMakingAction
VR班ハッカソン用リポジトリ

## キャラクターのシンプルな移動を実装（1/14）
 平面的なキャラクターの移動と走るアニメーションを実装。また、Chinemacineを導入して、カメラが壁を突き抜けないように実装。

## 敵の移動を実装（1/24）
　プレイヤーを追跡してくる敵の移動をナビゲーションを使って実装。一定の高さ以上の壁がある場合は、低い所を迂回してくる。
 
## 攻撃モーションアニメーションを実装（1/26）
 クリックで攻撃のアニメーションが再生される。右クリックで一部コンボが変わるように実装。

## プレイヤーからの攻撃の当たり判定および（2/6）
 アニメーションイベントを設定し、コライダーの有効・無効を切り替え。ナビゲーションで追跡してくる敵に体力（HP）を設定し、ヒットしたらダメージ処理をするように実装。
 
 キャラクターコントローラを使用した移動では、敵に乗り上げてしまうことが分かったので、Rigidbodyを使用した移動に変えた。

## プレイヤー攻撃にモーション値を適用&敵からの攻撃を実装（3/6）
 敵が攻撃し、プレイヤーにダメージを与えるシステムを作る前に、プレイヤーの攻撃にモーション値（特定の攻撃アニメーションでは、攻撃力に倍率をかける）を設定して、攻撃力が変動し、モーション値が高い攻撃ほど、ヒットストップ時間が長くなるように設定。
 
 敵は、プレイヤーに一定距離近づくと攻撃状態となり、攻撃のアニメーションを行う。当たり判定はプレイヤーの攻撃と同じようにアニメーションイベントを利用。
 
 プレイヤーはダメージを受けた際、体力が3分の1よりも大きい場合はのけぞることなく、攻撃を続けられる。しかし、3分の1以下になると大きくノックバックするようになり、ダメージを受けた時のエフェクトも強くなる。
 
## プレイヤーに遠距離攻撃モード実装（3/20）
 プレイヤーはShiftキーを押している間、遠距離攻撃モードになり、攻撃をすると斬撃を飛ばし、攻撃できる。また、斬撃のエフェクトもShaderGraghで実装。通常の攻撃時にShiftキーを押すと、通常攻撃をキャンセルして遠距離攻撃モードに遷移する。
