using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Script.Player {
	public class PlayerAnimationModel {
		private readonly float _targetValue;						// 目標値
		public float ReductionDuration { get; }					// 縮小にかかる時間
		public float ExpansionDuration { get; }					// 拡大にかかる時間

		public PlayerAnimationModel(float targetValue, float reductionDuration, float expansionDuration) {
			_targetValue = targetValue;
			ReductionDuration = reductionDuration;
			ExpansionDuration = expansionDuration;
		}
		
		public async UniTask DoAnimationAsync(GameObject targetGameObject, CancellationToken ct = default) {
			Tween tween = null;

			// 縮小
			tween = targetGameObject.transform.DOScaleY(_targetValue, ReductionDuration).SetEase(Ease.OutQuad);
			await using (ct.Register(() => tween.Kill())) await tween.AsyncWaitForCompletion();
			
			// 拡大
			tween = targetGameObject.transform.DOScaleY(1.0f, ExpansionDuration).SetEase(Ease.OutQuad);
			await using (ct.Register(() => tween.Kill())) await tween.AsyncWaitForCompletion();
		}
	}
}
